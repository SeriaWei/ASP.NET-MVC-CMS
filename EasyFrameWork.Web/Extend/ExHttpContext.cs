using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Easy.Web.Extend
{
    public static class ExHttpContext
    {
        public static void RegisterScript(this System.Web.UI.Page page, string key, string function)
        {
            page.ClientScript.RegisterStartupScript(page.GetType(), key, string.Format("<script type=\"text/javascript\">{0}</script>", function));
        }
        public static void RegisterScript(this HttpContextBase httpContext, string function)
        {
            httpContext.Response.Write(string.Format("<script type=\"text/javascript\">$(function(){{{0}}});</script>", function));
        }
    }
}
