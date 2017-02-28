/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Breadcrumb.Models;
using Easy.Extend;
using Easy.Web.CMS;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;
using System.Web.Mvc;

namespace Easy.CMS.Breadcrumb.Service
{
    public class BreadcrumbWidgetService : WidgetService<BreadcrumbWidget>
    {
        private IPageService _pageService;

        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }
        

        public List<PageEntity> ParentPages { get; set; }
        public override WidgetPart Display(WidgetBase widget, ControllerContext controllerContext)
        {
            if (ParentPages == null)
            {
                ParentPages = new List<PageEntity>();
                GetParentPage(controllerContext.HttpContext.GetLayout().Page);
            }

            return widget.ToWidgetPart(ParentPages);
        }

        void GetParentPage(PageEntity page)
        {
            ParentPages.Insert(0, page);
            if (page.ParentId.IsNotNullAndWhiteSpace() && page.ParentId != "#")
            {
                var parentPage = PageService.Get(m => m.ID == page.ParentId).FirstOrDefault();
                if (parentPage != null)
                {
                    GetParentPage(parentPage);
                }
            }
        }
    }
}