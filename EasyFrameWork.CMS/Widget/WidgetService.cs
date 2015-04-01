using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using System.Web;

namespace Easy.Web.CMS.Widget
{
    public class WidgetService : ServiceBase<WidgetBase>
    {
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
            var page = new Page.PageService().Get(pageId);
            var result = GetByLayoutId(page.LayoutId);
            List<WidgetBase> widgets = result.ToList();
            widgets.AddRange(this.Get(new DataFilter().Where("PageID", OperatorType.Equal, pageId)));
            return widgets;
        }
    }
    public abstract class WidgetService<T> : ServiceBase<T>, IWidgetPartDriver where T : WidgetBase
    {
        public WidgetService()
        {
            WidgetBaseService = new WidgetService();
        }
        public WidgetService WidgetBaseService { get; private set; }
        public override void Add(T item)
        {
            item.ID = Guid.NewGuid().ToString("N");
            WidgetBaseService.Add(item.ToWidgetBase());
            if (typeof(T) != typeof(WidgetBase))
            {
                base.Add(item);
            }
        }
        public override bool Update(T item, params object[] primaryKeys)
        {
            bool result = WidgetBaseService.Update(item.ToWidgetBase(), primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, primaryKeys);
            }
            return result;
        }
        public override bool Update(T item, DataFilter filter)
        {
            bool result = WidgetBaseService.Update(item.ToWidgetBase(), filter);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Update(item, filter);
            }
            return result;
        }
        public override IEnumerable<T> Get(DataFilter filter)
        {
            List<WidgetBase> widgetBases = WidgetBaseService.Get(filter).ToList();

            List<T> lists = new List<T>();
            if (typeof(T) != typeof(WidgetBase))
            {
                lists = base.Get(filter).ToList();
                for (int i = 0; i < widgetBases.Count; i++)
                {
                    CopyProperty(widgetBases[i], lists[i]);
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
                lists = base.Get(filter, pagin).ToList();
                for (int i = 0; i < widgetBases.Count(); i++)
                {
                    CopyProperty(widgetBases[i], lists[i]);
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
            int result = WidgetBaseService.Delete(filter);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Delete(filter);
            }
            return result;
        }
        public override int Delete(params object[] primaryKeys)
        {
            int result = WidgetBaseService.Delete(primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                return base.Delete(primaryKeys);
            }
            return result;
        }
        public override T Get(params object[] primaryKeys)
        {
            T model = base.Get(primaryKeys);
            if (typeof(T) != typeof(WidgetBase))
            {
                CopyProperty(WidgetBaseService.Get(primaryKeys), model);
            }
            return model;
        }
        private void CopyProperty(WidgetBase widget, T model)
        {
            model.AssemblyName = widget.AssemblyName;
            model.CreateBy = widget.CreateBy;
            model.CreatebyName = widget.CreatebyName;
            model.CreateDate = widget.CreateDate;
            model.Description = widget.Description;
            model.ID = widget.ID;
            model.LastUpdateBy = widget.LastUpdateBy;
            model.LastUpdateByName = widget.LastUpdateByName;
            model.LastUpdateDate = widget.LastUpdateDate;
            model.LayoutID = widget.LayoutID;
            model.PageID = widget.PageID;
            model.PartialView = widget.PartialView;
            model.Position = widget.Position;
            model.ServiceTypeName = widget.ServiceTypeName;
            model.Status = widget.Status;
            model.Title = widget.Title;
            model.ViewModelTypeName = widget.ViewModelTypeName;
            model.WidgetName = widget.WidgetName;
            model.ZoneID = widget.ZoneID;
        }
        public virtual WidgetBase GetWidget(WidgetBase widget)
        {
            T result = base.Get(widget.ID);
            CopyProperty(widget, result);
            return result;
        }

        public virtual WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            return widget.ToWidgetPart();
        }

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
    }
}
