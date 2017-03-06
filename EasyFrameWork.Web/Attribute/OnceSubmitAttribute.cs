/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace Easy.Web
{
    public class OnceSubmitAttribute : ActionFilterAttribute
    {
        string toKenKey = "__RequestVerificationToken";
        string redirect;
        bool always = false;
        public OnceSubmitAttribute(string redirectTo)
        {
            always = false;
            this.redirect = redirectTo;
        }
        public OnceSubmitAttribute(string redirectTo, bool always)
        {
            this.redirect = redirectTo;
            this.always = always;
        }
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            string token = null;
            string oldToken = null;
            if (filterContext.HttpContext.Request.Form.Count > 0)
            {
                if (always)
                {
                    foreach (string item in filterContext.HttpContext.Request.Form.AllKeys)
                    {
                        token += string.Format("{0}={1}&&", item, filterContext.HttpContext.Request.Form[item]);
                    }
                }
                else
                {
                    token = filterContext.HttpContext.Request.Form[toKenKey];
                }
                oldToken = filterContext.HttpContext.Session[toKenKey] == null ? null : filterContext.HttpContext.Session[toKenKey].ToString();
            }
            if (token != null && oldToken != null && token == oldToken)
            {
                filterContext.Result = new RedirectResult(redirect);
            }
            else
            {
                if (token != null)
                {
                    filterContext.HttpContext.Session[toKenKey] = token;
                }
                else filterContext.HttpContext.Session[toKenKey] = toKenKey;
                base.OnActionExecuting(filterContext);
            }

        }
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);
        }
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {
            base.OnResultExecuting(filterContext);
        }
        public override void OnResultExecuted(ResultExecutedContext filterContext)
        {
            filterContext.HttpContext.Response.Write(("<script type='text/javascript'> $('form').submit(function (e) {if ($(this).attr('submited')) {return false;}else {$(this).attr('submited', 'true');return true;}});</script>"));
            base.OnResultExecuted(filterContext);
        }
    }

}