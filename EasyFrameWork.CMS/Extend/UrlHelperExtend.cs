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
            query[name] = value;
            return "{0}?{1}".FormatWith(helper.RequestContext.HttpContext.Request.Url.AbsolutePath, query);
        }
    }
}
