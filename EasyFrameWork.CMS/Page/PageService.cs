/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using Easy.Constant;
using Easy.Data;
using Easy.Extend;
using Easy.RepositoryPattern;
using Easy.Web.CMS.DataArchived;
using Easy.Web.CMS.ExtendField;
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
        private IDataArchivedService _dataArchivedService;

        public IDataArchivedService DataArchivedService
        {
            get { return _dataArchivedService ?? (_dataArchivedService = ServiceLocator.Current.GetInstance<IDataArchivedService>()); }
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
            Update(new PageEntity { IsPublish = true },
               new DataFilter(new List<string> { "IsPublish" })
               .Where("ID", OperatorType.Equal, item.ID));

            //Delete(m => m.ReferencePageID == item.ID && m.IsPublishedPage == true);

            DataArchivedService.Delete(CacheTrigger.PageWidgetsArchivedKey.FormatWith(item.ID));

            item.ReferencePageID = item.ID;
            item.IsPublishedPage = true;
            item.PublishDate = DateTime.Now;
            if (item.ExtendFields != null)
            {
                item.ExtendFields.Each(m => m.ActionType = ActionType.Create);
            }
            var widgets = WidgetService.GetByPageId(item.ID);
            Add(item);
            widgets.Each(m =>
            {
                var widgetService = m.CreateServiceInstance();
                m = widgetService.GetWidget(m);
                if (m.ExtendFields != null)
                {
                    m.ExtendFields.Each(f => f.ActionType = ActionType.Create);
                }
                m.PageID = item.ID;
                widgetService.Publish(m);
            });
        }

        public void Revert(string ID)
        {
            var page = Get(ID);
            if (page.IsPublishedPage)
            {
                Update(new PageEntity { PublishDate = null }, new DataFilter(new List<string> { "PublishDate" }).Where("ID", OperatorType.Equal, page.ReferencePageID));

                Delete(page.ReferencePageID);//删除当前的编辑版本，加入旧的版本做为编辑版本，再发布
                page.ID = page.ReferencePageID;
                page.ReferencePageID = null;
                page.IsPublish = false;
                page.IsPublishedPage = false;
                if (page.ExtendFields != null)
                {
                    page.ExtendFields.Each(m => m.ActionType = ActionType.Create);
                }
                base.Add(page);
                var widgets = WidgetService.GetByPageId(ID);
                widgets.Each(m =>
                {
                    var widgetService = m.CreateServiceInstance();
                    m = widgetService.GetWidget(m);
                    if (m.ExtendFields != null)
                    {
                        m.ExtendFields.Each(f => f.ActionType = ActionType.Create);
                    }
                    m.PageID = page.ID;
                    widgetService.Publish(m);
                });
                Publish(page);
            }
        }
        public override int Delete(DataFilter filter)
        {
            var deletes = Get(filter).ToList(m => m.ID);
            if (deletes.Any() && Get(new DataFilter().Where("ParentId", OperatorType.In, deletes)).Any())
            {
                Delete(new DataFilter().Where("ParentId", OperatorType.In, deletes));
                Delete(new DataFilter().Where("ReferencePageID", OperatorType.In, deletes));
            }
            if (deletes.Any())
            {
                var widgets = WidgetService.Get(new DataFilter().Where("PageID", OperatorType.In, deletes));
                widgets.Each(m => m.CreateServiceInstance().DeleteWidget(m.ID));

                deletes.Each(p => DataArchivedService.Delete(CacheTrigger.PageWidgetsArchivedKey.FormatWith(p)));
            }
            return base.Delete(filter);
        }
        public override int Delete(params object[] primaryKeys)
        {
            PageEntity page = Get(primaryKeys);
            if (page != null)
            {
                Delete(m => m.ParentId == page.ID);
                var widgets = WidgetService.Get(m => m.PageID == page.ID);
                widgets.Each(m => m.CreateServiceInstance().DeleteWidget(m.ID));
                if (page.PublishDate.HasValue)
                {
                    Delete(m => m.ReferencePageID == page.ID);
                }
                DataArchivedService.Delete(CacheTrigger.PageWidgetsArchivedKey.FormatWith(page.ID));
            }


            return base.Delete(primaryKeys);
        }

        public void DeleteVersion(string ID)
        {
            PageEntity page = Get(ID);
            if (page != null)
            {
                var widgets = WidgetService.Get(m => m.PageID == page.ID);
                widgets.Each(m => m.CreateServiceInstance().DeleteWidget(m.ID));
                DataArchivedService.Delete(CacheTrigger.PageWidgetsArchivedKey.FormatWith(page.ID));
            }
            base.Delete(ID);
        }
        public void Move(string id, int position, int oldPosition)
        {
            var page = Get(id);
            page.DisplayOrder = position;
            var filter = new DataFilter()
                .Where("IsPublishedPage", OperatorType.Equal, false)
                .Where("ParentId", OperatorType.Equal, page.ParentId)
                .Where("Id", OperatorType.NotEqual, page.ID);
            if (position > oldPosition)
            {
                filter.Where("DisplayOrder", OperatorType.LessThanOrEqualTo, position);
                filter.Where("DisplayOrder", OperatorType.GreaterThanOrEqualTo, oldPosition);
                var pages = Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder--;
                    Update(m);
                });
            }
            else
            {
                filter.Where("DisplayOrder", OperatorType.LessThanOrEqualTo, oldPosition);
                filter.Where("DisplayOrder", OperatorType.GreaterThanOrEqualTo, position);
                var pages = Get(filter);
                pages.Each(m =>
                {
                    m.DisplayOrder++;
                    Update(m);
                });
            }
            Update(page);
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
                path = "~/index";
            }
            filter.Where("Url", OperatorType.Equal, (path.StartsWith("~") ? "" : "~") + path);
            filter.Where("IsPublishedPage", OperatorType.Equal, !isPreView).OrderBy("PublishDate", OrderType.Descending);
            var pages = Get(filter, new Pagination { PageSize = 1 });

            var result = pages.FirstOrDefault();
            if (result != null && result.ExtendFields != null)
            {
                /* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
                ((List<ExtendFieldEntity>)result.ExtendFields).Add(new ExtendFieldEntity { Title = "meta_support", Value = "ZKEASOFT" });
            }
            return result;
        }

        public void MarkChanged(string pageId)
        {
            Update(new PageEntity { IsPublish = false, LastUpdateDate = DateTime.Now, LastUpdateBy = ApplicationContext.CurrentUser.UserID },
              new DataFilter(new List<string> { "IsPublish", "LastUpdateDate", "LastUpdateBy" })
              .Where("ID", OperatorType.Equal, pageId));
        }

    }
}
