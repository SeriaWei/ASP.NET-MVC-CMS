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
            if (query[name] == value)
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
        public static string PostUrl(this UrlHelper helper, string url, int id)
        {
            return PostUrl(helper, url, id.ToString());
        }
        public static string PostUrl(this UrlHelper helper, string url, string id)
        {
            if (url.IsNullOrWhiteSpace())
            {
                url = "/";
            }
            return url + (url.EndsWith("/") ? "" : "/") + "post-" + id;
        }
        public static string CategoryUrl(this UrlHelper helper, int id)
        {
            return CategoryUrl(helper, id.ToString());
        }
        public static string CategoryUrl(this UrlHelper helper, string id)
        {
            string url = helper.RequestContext.RouteData.GetPath();
            string currentCategory = helper.RequestContext.RouteData.GetCategory().ToString();
            if (currentCategory != id)
            {
                return url + (url.EndsWith("/") ? "" : "/") + "cate-" + id;
            }
            else
            {
                return url;
            }
        }
        public static string Page(this UrlHelper helper, int pageIndex)
        {
            var category = helper.RequestContext.RouteData.GetCategory();
            if (category > 0)
            {
                return helper.RequestContext.RouteData.GetPath() + "/cate-" + category + "/p-" + pageIndex;
            }
            return helper.RequestContext.RouteData.GetPath() + "/p-" + pageIndex;
        }
    }
}
