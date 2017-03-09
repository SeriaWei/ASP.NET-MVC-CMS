/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web.Mvc;
using Easy.Web.CMS.Page;

namespace Easy.Web.CMS.Filter
{
    public class ViewPageAttribute : WidgetAttribute
    {
        public override PageEntity GetPage(ActionExecutedContext filterContext)
        {
            string pageId = filterContext.RouteData.Values["id"].ToString();
            return PageService.Get(pageId);
        }

        public override PageViewMode GetPageViewMode()
        {
            return PageViewMode.ViewOnly;
        }
    }

}
