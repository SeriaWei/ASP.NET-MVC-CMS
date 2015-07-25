using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web.Mvc;

namespace Easy.Web.Attribute
{
    public class PopUpAttribute : ActionFilterAttribute, IActionFilter
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            ViewResult viewResult = (filterContext.Result as ViewResult);
            if (viewResult != null)
            {
                viewResult.MasterName = "~/Views/Shared/_PopUpLayout.cshtml";
            }
        }

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

        }


    }
}
