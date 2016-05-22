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

namespace Easy.Web.CMS.Widget
{
    public class WidgetService : ServiceBase<WidgetBase>, IWidgetService
    {

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

    }
    public abstract class WidgetService<T> : ServiceBase<T>, IWidgetPartDriver where T : WidgetBase
    {
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
    }
}
