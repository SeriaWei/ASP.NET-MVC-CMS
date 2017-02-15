/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.CMS
{
    public static class UrlHelperExtend
    {
        public static string PathContent(this UrlHelper helper, string path)
        {
            return helper.Content(path.IsNullOrEmpty() ? "~/" : path);
        }

        public static string ValidateCode(this UrlHelper helper)
        {
            return helper.Action("Code", "Validation", new { module = "Validation" });
        }

        public static string AddQueryToCurrentUrl(this UrlHelper helper, string name, string value)
        {
            var query = HttpUtility.ParseQueryString(helper.RequestContext.HttpContext.Request.Url.Query);
            if(query[name] == value)
            {
                query.Remove(name);
            }
            else
            {
                query[name] = value;
            }
            if (query.AllKeys.Length > 0)
            {
                return "{0}?{1}".FormatWith(helper.RequestContext.HttpContext.Request.Url.AbsolutePath, query);
            }
            else
            {
                return helper.RequestContext.HttpContext.Request.Url.AbsolutePath;
            }
        }
    }
}
