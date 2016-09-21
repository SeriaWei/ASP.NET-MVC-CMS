using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.CMS.Breadcrumb.Models;
using Easy.Extend;
using Easy.Web.CMS;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Widget;
using Microsoft.Practices.ServiceLocation;

namespace Easy.CMS.Breadcrumb.Service
{
    public class BreadcrumbWidgetService : WidgetService<BreadcrumbWidget>
    {
        private IPageService _pageService;

        public IPageService PageService
        {
            get { return _pageService ?? (_pageService = ServiceLocator.Current.GetInstance<IPageService>()); }
        }

        private List<PageEntity> _parentPages;

        public List<PageEntity> ParentPages { get; set; }
        public override WidgetPart Display(WidgetBase widget, HttpContextBase httpContext)
        {
            if (ParentPages == null)
            {
                ParentPages = new List<PageEntity>();
                GetParentPage(httpContext.GetLayout().Page);
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