using Easy.Data;
using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using Easy;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Layout;
using Easy.Constant;
using Easy.Extend;
using Microsoft.Practices.ServiceLocation;

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
