using System.Web.Mvc;
using Easy.Web.CMS.Page;

namespace Easy.Web.CMS.Filter
{
    public class EditWidgetAttribute : WidgetAttribute
    {
        public override PageEntity GetPage(ActionExecutedContext filterContext)
        {
            string pageId = filterContext.RequestContext.HttpContext.Request.QueryString["ID"];
            return PageService.Get(pageId);
        }

        public override string GetLayout()
        {
            return "~/Modules/Common/Views/Shared/_DesignPageLayout.cshtml";
        }
    }

}
