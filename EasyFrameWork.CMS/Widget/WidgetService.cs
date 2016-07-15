using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using System.Web;
using Easy.Cache;
using Easy.Extend;
using Easy.Web.CMS.Page;
using Microsoft.Practices.ServiceLocation;
using EasyZip;
using Newtonsoft.Json;
using System.IO;
using System.Configuration;
using System.Security.Cryptography;

namespace Easy.Web.CMS.Widget
{
    public class WidgetService : ServiceBase<WidgetBase>, IWidgetService
    {
        protected const string TempFolder = "~/Temp";
        protected const string TempJsonFile = "~/Temp/{0}-widget.json";
        protected const string WidgetSalt = "WidgetSalt";

        private void TriggerPage(WidgetBase widget)
        {
            if (widget != null && widget.PageID.IsNotNullAndWhiteSpace())
            {
                PageService.MarkChanged(widget.PageID);
            }
        }

        private IPageService _pageService;

        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }
        public IEnumerable<WidgetBase> GetByLayoutId(string layoutId)
        {
            return this.Get(new DataFilter().Where("LayoutID", OperatorType.Equal, layoutId));
        }
        public IEnumerable<WidgetBase> GetByPageId(string pageId)
        {
            return this.Get(new DataFilter().Where("PageID", OperatorType.Equal, pageId));
        }
        public IEnumerable<WidgetBase> GetAllByPageId(string pageId)
        {
            var page = PageService.Get(pageId);
            return GetAllByPage(page);
        }

        public IEnumerable<WidgetBase> GetAllByPage(PageEntity page)
        {
            var result = GetByLayoutId(page.LayoutId);
            List<WidgetBase> widgets = result.ToList();
            widgets.AddRange(GetByPageId(page.ID));
            return widgets;
        }
        public override void Add(WidgetBase item)
        {
            base.Add(item);
            TriggerPage(item);
        }

        public override bool Update(WidgetBase item, DataFilter filter)
        {
            Get(filter).Each(TriggerPage);
            return base.Update(item, filter);
        }

        public override bool Update(WidgetBase item, params object[] primaryKeys)
        {
            TriggerPage(item);
            return base.Update(item, primaryKeys);
        }

        public override int Delete(params object[] primaryKeys)
        {
            TriggerPage(Get(primaryKeys));
            return base.Delete(primaryKeys);
        }

        public override int Delete(DataFilter filter)
        {
            Get(filter).Each(TriggerPage);
            return base.Delete(filter);
        }

        public override int Delete(Expression<Func<WidgetBase, bool>> expression)
        {
            Get(expression).Each(TriggerPage);
            return base.Delete(expression);
        }

        public override int Delete(WidgetBase item)
        {
            TriggerPage(item);
            return base.Delete(item);

        }


        public WidgetPart ApplyTemplate(WidgetBase widget, HttpContextBase httpContext)
        {
            var widgetBase = Get(widget.ID);
            var service = widgetBase.CreateServiceInstance();
            widgetBase = service.GetWidget(widgetBase);

            widgetBase.PageID = widget.PageID;
            widgetBase.ZoneID = widget.ZoneID;
            widgetBase.Position = widget.Position;
            widgetBase.IsTemplate = false;
            widgetBase.Thumbnail = null;
            widgetBase.LayoutID = null;

            var widgetPart = service.Display(widgetBase, httpContext);
            service.AddWidget(widgetBase);
            return widgetPart;
        }

        public ZipFile PackWidget(string widgetId)
        {
            var widgetBase = Get(widgetId);
            var zipfile = widgetBase.CreateServiceInstance().PackWidget(widgetBase);
            var bytes = Encrypt(zipfile.ToMemoryStream().ToArray());
            string tempFile = ((CMSApplicationContext)ApplicationContext).MapPath(TempJsonFile.FormatWith(Guid.NewGuid().ToString("N")));
            string folder = ((CMSApplicationContext)ApplicationContext).MapPath(TempFolder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            Directory.GetFiles(folder).Each(f =>
            {
                try
                {
                    File.Delete(f);
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            });
            File.WriteAllBytes(tempFile, bytes);
            ZipFile zipFile = new ZipFile();
            zipFile.AddFile(new FileInfo(tempFile));
            return zipFile;
        }

        public WidgetBase InstallPackWidget(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);
            var bytesReal = Decrypt(bytes);
            ZipFile zipFile = new ZipFile();
            var files = zipFile.ToFileCollection(new MemoryStream(bytesReal));
            foreach (ZipFileInfo item in files)
            {
                if (item.RelativePath.EndsWith("-widget.json"))
                {
                    try
                    {
                        var jsonStr = Encoding.UTF8.GetString(item.FileBytes);
                        var widgetBase = JsonConvert.DeserializeObject<WidgetBase>(jsonStr);
                        var service = widgetBase.CreateServiceInstance();
                        var widget = service.UnPackWidget(files);
                        widget.PageID = null;
                        widget.LayoutID = null;
                        widget.ZoneID = null;
                        widget.IsSystem = false;
                        widget.IsTemplate = true;
                        service.AddWidget(widget);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                        return null;
                    }
                }
            }
            return null;
        }

        private byte[] Encrypt(byte[] source)
        {
            var salt = ConfigurationManager.AppSettings[WidgetSalt];
            if (salt != null)
            {
                var param = new CspParameters { KeyContainerName = salt };
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(param))
                {
                    byte[] plaindata = Encoding.Default.GetBytes(salt);
                    byte[] encryptdata = rsa.Encrypt(plaindata, false);
                    byte[] combined = new byte[encryptdata.Length + source.Length];
                    Buffer.BlockCopy(encryptdata, 0, combined, 0, encryptdata.Length);
                    Buffer.BlockCopy(source, 0, combined, encryptdata.Length, source.Length);
                    return MarkData(combined);
                }
            }
            return source;
        }

        private byte[] Decrypt(byte[] source)
        {
            if (IsEncrypt(source))
            {
                var salt = ConfigurationManager.AppSettings[WidgetSalt];
                var bytesWithsSalt = ClearDataMark(source);

            }
            return source;
        }

        private byte[] MarkData(byte[] source)
        {
            byte[] newBytes = new byte[source.Length + 200];
            for (int i = 0; i < newBytes.Length; i++)
            {
                if (i < 100 || i > newBytes.Length - 100 - 1)
                {
                    newBytes[i] = 0;
                }
                else
                {
                    newBytes[i] = source[i - 100];
                }
            }
            return newBytes;
        }

        private byte[] ClearDataMark(byte[] source)
        {
            byte[] newBytes = new byte[source.Length - 200];
            for (int i = 100; i < source.Length - 100; i++)
            {
                newBytes[i - 100] = source[i];
            }
            return newBytes;
        }
        private bool IsEncrypt(byte[] source)
        {
            for (int i = 0; i < 100; i++)
            {
                if (source[i] != 0 || source[source.Length - i - 1] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
    public abstract class WidgetService<T> : ServiceBase<T>, IWidgetPartDriver where T : WidgetBase
    {
        protected const string TempFolder = "~/Temp";
        protected const string TempJsonFile = "~/Temp/{0}-widget.json";
        protected WidgetService()
        {
            WidgetBaseService = ServiceLocator.Current.GetInstance<IWidgetService>();
        }
        public IWidgetService WidgetBaseService { get; private set; }

        private void CopyTo(WidgetBase from, T to)
        {
            if (to != null)
            {
                to.AssemblyName = from.AssemblyName;
                to.CreateBy = from.CreateBy;
                to.CreatebyName = from.CreatebyName;
                to.CreateDate = from.CreateDate;
                to.Description = from.Description;
                to.ID = from.ID;
                to.LastUpdateBy = from.LastUpdateBy;
                to.LastUpdateByName = from.LastUpdateByName;
                to.LastUpdateDate = from.LastUpdateDate;
                to.LayoutID = from.LayoutID;
                to.PageID = from.PageID;
                to.PartialView = from.PartialView;
                to.Position = from.Position;
                to.ServiceTypeName = from.ServiceTypeName;
                to.Status = from.Status;
                to.Title = from.Title;
                to.ViewModelTypeName = from.ViewModelTypeName;
                to.WidgetName = from.WidgetName;
                to.ZoneID = from.ZoneID;
                to.FormView = from.FormView;
                to.StyleClass = from.StyleClass;
                to.IsTemplate = from.IsTemplate;
                to.Thumbnail = from.Thumbnail;
                to.IsSystem = from.IsSystem;
                to.ExtendFields = from.ExtendFields;
            }
        }

        public override void Add(T item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            WidgetBaseService.Add(item);
            if (typeof(T) != typeof(WidgetBase))
            {
                base.Add(item);
            }
        }
        public override bool Update(T item, params object[] primaryKeys)
        {
            bool result = WidgetBaseService.Update(item, primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, primaryKeys);
            }
            Signal.Trigger(CacheTrigger.WidgetChanged);
            return result;
        }
        public override bool Update(T item, DataFilter filter)
        {
            bool result = WidgetBaseService.Update(item, filter);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, filter);
            }
            Signal.Trigger(CacheTrigger.WidgetChanged);
            return result;
        }
        public override IEnumerable<T> Get(DataFilter filter)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter).ToList();

            List<T> lists = new List<T>();
            if (typeof(T) != typeof(WidgetBase))
            {
                lists = base.Get(new DataFilter().Where("ID", OperatorType.In, widgetBases.ToList(m => m.ID))).ToList();
                for (int i = 0; i < widgetBases.Count; i++)
                {
                    CopyTo(widgetBases[i], lists[i]);
                }
            }
            else
            {
                widgetBases.ForEach(m => lists.Add(m as T));
            }
            return lists;

        }
        public override IEnumerable<T> Get(DataFilter filter, Pagination pagin)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter, pagin).ToList();
            List<T> lists = new List<T>();
            if (typeof(T) != typeof(WidgetBase))
            {
                lists = base.Get(new DataFilter().Where("ID", OperatorType.In, widgetBases.ToList(m => m.ID)), pagin).ToList();
                for (int i = 0; i < widgetBases.Count(); i++)
                {
                    CopyTo(widgetBases[i], lists[i]);
                }
            }
            else
            {
                widgetBases.ForEach(m => lists.Add(m as T));
            }
            return lists;
        }
        public override int Delete(DataFilter filter)
        {
            if (typeof(T) != typeof(WidgetBase))
            {
                base.Delete(filter);
            }
            int result = WidgetBaseService.Delete(filter);
            Signal.Trigger(CacheTrigger.WidgetChanged);
            return result;
        }
        public override int Delete(params object[] primaryKeys)
        {
            if (typeof(T) != typeof(WidgetBase))
            {
                base.Delete(primaryKeys);
            }
            int result = WidgetBaseService.Delete(primaryKeys);
            Signal.Trigger(CacheTrigger.WidgetChanged);
            return result;
        }
        public override T Get(params object[] primaryKeys)
        {
            T model = base.Get(primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                CopyTo(WidgetBaseService.Get(primaryKeys), model);
            }
            return model;
        }
        public virtual WidgetBase GetWidget(WidgetBase widget)
        {
            T result = base.Get(widget.ID);
            CopyTo(widget, result);
            return result;
        }

        public virtual WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            return widget.ToWidgetPart();
        }

        #region PartDrive
        public virtual void AddWidget(WidgetBase widget)
        {
            this.Add((T)widget);
        }


        public virtual void DeleteWidget(string widgetId)
        {
            this.Delete(widgetId);
        }

        public virtual void UpdateWidget(WidgetBase widget)
        {
            this.Update((T)widget);
        }
        #endregion

        public virtual void Publish(WidgetBase widget)
        {
            AddWidget(widget);
        }

        #region PackWidget
        public virtual ZipFile PackWidget(WidgetBase widget)
        {
            widget = GetWidget(widget);
            widget.PageID = null;
            widget.LayoutID = null;
            widget.ZoneID = null;
            widget.IsSystem = false;
            widget.IsTemplate = true;
            var jsonResult = JsonConvert.SerializeObject(widget);
            string tempFile = ((CMSApplicationContext)ApplicationContext).MapPath(TempJsonFile.FormatWith(Guid.NewGuid().ToString("N")));
            if (!Directory.Exists(((CMSApplicationContext)ApplicationContext).MapPath(TempFolder)))
            {
                Directory.CreateDirectory(((CMSApplicationContext)ApplicationContext).MapPath(TempFolder));
            }
            File.WriteAllText(tempFile, jsonResult);
            ZipFile file = new ZipFile();
            file.AddFile(new FileInfo(tempFile));
            return file;
        }

        public virtual WidgetBase UnPackWidget(ZipFileInfoCollection pack)
        {
            WidgetBase result = null;
            try
            {
                foreach (ZipFileInfo item in pack)
                {
                    if (item.RelativePath.EndsWith("-widget.json"))
                    {
                        var jsonStr = Encoding.UTF8.GetString(item.FileBytes);
                        var widgetBase = JsonConvert.DeserializeObject<WidgetBase>(jsonStr);
                        var widget = JsonConvert.DeserializeObject(jsonStr, widgetBase.GetViewModelType()) as WidgetBase;
                        result = widget;
                    }
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return result;
        }
        #endregion
    }
}
