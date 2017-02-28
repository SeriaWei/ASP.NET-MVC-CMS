/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using Easy.Data;
using Easy.Extend;
using Easy.Web.CMS.Widget;
using System.Web.Routing;

namespace Easy.Web.CMS
{
    public static class RouteDataExtend
    {
        public static string GetPath(this RouteData roteData)
        {
            if (roteData.Values.ContainsKey("path"))
            {
                return "/" + roteData.Values["path"].ToString();
            }
            return "/";
        }
        public static int GetPost(this RouteData roteData)
        {
            int post = 0;
            if (roteData.Values.ContainsKey("post"))
            {
                int.TryParse(roteData.Values["post"].ToString(), out post);
            }
            return post;
        }
        public static int GetCategory(this RouteData roteData)
        {
            int post = 0;
            if (roteData.Values.ContainsKey("category"))
            {
                int.TryParse(roteData.Values["category"].ToString(), out post);
            }
            return post;
        }
        public static int GetPage(this RouteData roteData)
        {
            int page = 0;
            if (roteData.Values.ContainsKey("page"))
            {
                int.TryParse(roteData.Values["page"].ToString(), out page);
            }
            return page;
        }
    }
}
