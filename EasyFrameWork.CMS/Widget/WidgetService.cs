using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using Easy.Cache;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.DataArchived;
using Easy.Web.CMS.Encrypt;
using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Page;
using EasyZip;
using Microsoft.Practices.ServiceLocation;
using Newtonsoft.Json;

namespace Easy.Web.CMS.Widget
{
    public class WidgetService : ServiceBase<WidgetBase>, IWidgetService
    {
        protected const string EncryptWidgetTemplate = "EncryptWidgetTemplate";

        private void TriggerChange(WidgetBase widget)
        {
            if (widget != null && widget.PageID.IsNotNullAndWhiteSpace())
            {
                PageService.MarkChanged(widget.PageID);
            }
            else if (widget != null && widget.LayoutID.IsNotNullAndWhiteSpace())
            {
                LayoutService.MarkChanged(widget.LayoutID);
            }
        }
        private ILayoutService _layoutService;

        public ILayoutService LayoutService
        {
            get { return _layoutService ?? (_layoutService = ServiceLocator.Current.GetInstance<ILayoutService>()); }
        }
        private IPageService _pageService;

        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }

        private IEncryptService _encryptService;

        public IEncryptService EncryptService
        {
            get { return _encryptService ?? (_encryptService = ServiceLocator.Current.GetInstance<IEncryptService>()); }
        }
        private IDataArchivedService _dataArchivedService;

        public IDataArchivedService DataArchivedService
        {
            get { return _dataArchivedService ?? (_dataArchivedService = ServiceLocator.Current.GetInstance<IDataArchivedService>()); }
        }
        public IEnumerable<WidgetBase> GetByLayoutId(string layoutId)
        {
            return Get(new DataFilter().Where("LayoutID", OperatorType.Equal, layoutId));
        }
        public IEnumerable<WidgetBase> GetByPageId(string pageId)
        {
            return Get(new DataFilter().Where("PageID", OperatorType.Equal, pageId));
        }
        public IEnumerable<WidgetBase> GetAllByPageId(string pageId)
        {
            var page = PageService.Get(pageId);
            return GetAllByPage(page);
        }

        public IEnumerable<WidgetBase> GetAllByPage(PageEntity page)
        {
            Func<PageEntity, List<WidgetBase>> getPageWidgets = p =>
            {
                var result = GetByLayoutId(p.LayoutId);
                List<WidgetBase> widgets = result.ToList();
                widgets.AddRange(GetByPageId(p.ID));
                return widgets.Select(widget => widget.CreateServiceInstance().GetWidget(widget)).ToList();
            };
            if (page.IsPublishedPage)
            {
                return DataArchivedService.Get(CacheTrigger.PageWidgetsArchivedKey.FormatWith(page.ReferencePageID), () => getPageWidgets(page));    
            }
            return getPageWidgets(page);
        }
        public override void Add(WidgetBase item)
        {
            base.Add(item);
            TriggerChange(item);
        }

        public override bool Update(WidgetBase item, DataFilter filter)
        {
            Get(filter).Each(TriggerChange);
            return base.Update(item, filter);
        }

        public override bool Update(WidgetBase item, params object[] primaryKeys)
        {
            TriggerChange(item);
            return base.Update(item, primaryKeys);
        }

        public override int Delete(params object[] primaryKeys)
        {
            TriggerChange(Get(primaryKeys));
            return base.Delete(primaryKeys);
        }

        public override int Delete(DataFilter filter)
        {
            Get(filter).Each(TriggerChange);
            return base.Delete(filter);
        }

        public override int Delete(Expression<Func<WidgetBase, bool>> expression)
        {
            Get(expression).Each(TriggerChange);
            return base.Delete(expression);
        }

        public override int Delete(WidgetBase item)
        {
            TriggerChange(item);
            return base.Delete(item);

        }


        public WidgetPart ApplyTemplate(WidgetBase widget, HttpContextBase httpContext)
        {
            var widgetBase = Get(widget.ID);
            if (widgetBase == null) return null;
            if (widgetBase.ExtendFields != null)
            {
                widgetBase.ExtendFields.Each(f => { f.ActionType = ActionType.Create; });
            }
            var service = widgetBase.CreateServiceInstance();
            widgetBase = service.GetWidget(widgetBase);

            widgetBase.PageID = widget.PageID;
            widgetBase.ZoneID = widget.ZoneID;
            widgetBase.Position = widget.Position;
            widgetBase.LayoutID = widget.LayoutID;
            widgetBase.IsTemplate = false;
            widgetBase.Thumbnail = null;

            var widgetPart = service.Display(widgetBase, httpContext);
            service.AddWidget(widgetBase);
            return widgetPart;
        }

        public MemoryStream PackWidget(string widgetId)
        {
            var widgetBase = Get(widgetId);
            var zipfile = widgetBase.CreateServiceInstance().PackWidget(widgetBase);
            var bytes = Encrypt(zipfile.ToMemoryStream().ToArray());
            return new MemoryStream(bytes);
        }

        public WidgetBase InstallPackWidget(Stream stream)
        {
            byte[] bytes = new byte[stream.Length];
            stream.Read(bytes, 0, bytes.Length);
            stream.Seek(0, SeekOrigin.Begin);

            ZipFile zipFile = new ZipFile();

            var files = zipFile.ToFileCollection(new MemoryStream(Decrypt(bytes)));
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
            if (ConfigurationManager.AppSettings[EncryptWidgetTemplate] == "true")
            {
                return EncryptService.Encrypt(source);
            }
            return source;
        }

        private byte[] Decrypt(byte[] source)
        {
            return EncryptService.Decrypt(source);
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
            Add((T)widget);
        }


        public virtual void DeleteWidget(string widgetId)
        {
            Delete(widgetId);
        }

        public virtual void UpdateWidget(WidgetBase widget)
        {
            Update((T)widget);
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
