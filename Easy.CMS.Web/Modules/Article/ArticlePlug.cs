using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Article
{
    public class ArticlePlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> Regist()
        {
            var routes = new List<RouteDescriptor>();

            routes.Add(new RouteDescriptor
            {
                RouteName = "articleAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new {  action = "index", module = "Article" },
                Namespaces = new string[] { "Easy.Web.CMS.Article.Controllers" },
                Priority = 1
            });
            return routes;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            throw new NotImplementedException();
        }

        public override void InitScript()
        {
            
        }

        public override void InitStyle()
        {
            Style("Article").Include("~/Modules/Article/Content/Article.css");
        }
    }
}