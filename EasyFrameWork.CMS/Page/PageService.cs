using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Data;
using Easy.RepositoryPattern;
using Easy.Extend;
using Easy.Constant;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Page
{
    public class PageService : ServiceBase<PageEntity>, IPageService
    {
        private IWidgetService _widgetService;

        public IWidgetService WidgetService
        {
            get { return _widgetService ?? (_widgetService = ServiceLocator.Current.GetInstance<IWidgetService>()); }
        }
        public override void Add(PageEntity item)
        {
            if (!item.IsPublishedPage && Count(m => m.Url == item.Url && m.IsPublishedPage == false) > 0)
            {
                throw new PageExistException(item);
            }
            item.ID = Guid.NewGuid().ToString("N");
            if (item.ParentId.IsNullOrEmpty())
            {
                item.ParentId = "#";
            }
            base.Add(item);
        }

        public override bool Update(PageEntity item, params object[] primaryKeys)
        {
            if (Count(m => m.ID != item.ID && m.Url == item.Url && m.IsPublishedPage == false) > 0)
            {
                throw new PageExistException(item);
            }
            item.IsPublish = false;
            return base.Update(item, primaryKeys);
        }

        public void Publish(PageEntity item)
        {
            this.Update(new PageEntity { IsPublish = true, PublishDate = DateTime.Now },
               new DataFilter(new List<string> { "IsPublish", "PublishDate" })
               .Where("ID", OperatorType.Equal, item.ID));

            this.Delete(m => m.Url == item.Url && m.IsPublishedPage == true);

            item.IsPublishedPage = true;
            item.PublishDate = DateTime.Now;
            var widgets = WidgetService.GetByPageId(item.ID);
            Add(item);
            widgets.Each(m =>
            {
                var widgetService = m.CreateServiceInstance();
                m = widgetService.GetWidget(m);
                m.PageID = item.ID;
                widgetService.Publish(m);
            });
        }
        public override int Delete(DataFilter filter)
        {
            var deletes = this.Get(filter).ToList(m => m.ID);
            if (deletes.Any() && this.Get(new DataFilter().Where("ParentId", OperatorType.In, deletes)).Any())
            {
                this.Delete(new DataFilter().Where("ParentId", OperatorType.In, deletes));
            }
            if (deletes.Any())
            {
                var widgets = WidgetService.Get(new DataFilter().Where("PageID", OperatorType.In, deletes));
                widgets.Each(m => m.CreateServiceInstance().DeleteWidget(m.ID));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            PageEntity page = Get(primaryKeys);
            this.Delete(new DataFilter().Where("ParentId", OperatorType.Equal, page.ID));

            var widgets = WidgetService.Get(new DataFilter().Where("PageID", OperatorType.Equal, page.ID));
            widgets.Each(m => m.CreateServiceInstance().DeleteWidget(m.ID));
            return base.Delete(primaryKeys);
        }

        public void Move(string id, int position, int oldPosition)
        {
            var page = this.Get(id);
            page.DisplayOrder = position;
            var filter = new DataFilter()
                .Where("ParentId", OperatorType.Equal, page.ParentId)
                .Where("Id", OperatorType.NotEqual, page.ID);
            if (position > oldPosition)
            {
                filter.Where("DisplayOrder", OperatorType.LessThanOrEqualTo, position);
                filter.Where("DisplayOrder", OperatorType.GreaterThanOrEqualTo, oldPosition);
                var pages = this.Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder--;
                    this.Update(m);
                });
            }
            else
            {
                filter.Where("DisplayOrder", OperatorType.LessThanOrEqualTo, oldPosition);
                filter.Where("DisplayOrder", OperatorType.GreaterThanOrEqualTo, position);
                var pages = this.Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder++;
                    this.Update(m);
                });
            }
            this.Update(page);
        }
        public PageEntity GetByPath(string path, bool isPreView)
        {
            if (path != "/" && path.EndsWith("/"))
            {
                path = path.Substring(0, path.Length - 1);
            }
            var filter = new DataFilter();

            if (path == "/")
            {
                filter.Where("IsHomePage", OperatorType.Equal, true);
            }
            else
            {
                filter.Where("Url", OperatorType.Equal, (path.StartsWith("~") ? "" : "~") + path);
            }

            filter.Where("IsPublishedPage", OperatorType.Equal, !isPreView);
            var pages = Get(filter);

            return pages.FirstOrDefault();
        }

        public void MarkChanged(string pageId)
        {
            this.Update(new PageEntity { IsPublish = false },
              new DataFilter(new List<string> { "IsPublish" })
              .Where("ID", OperatorType.Equal, pageId));
        }
    }
}
