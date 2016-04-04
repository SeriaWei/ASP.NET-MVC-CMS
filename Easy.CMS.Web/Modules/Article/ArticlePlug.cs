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
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            return null;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "文章管理",
                Icon = "glyphicon-font",
                Order = 10,
                Children = new List<AdminMenu>
                {
                    new AdminMenu
                    {
                        Title = "文章列表",
                        Url = "~/admin/Article",
                        Icon = "glyphicon-align-justify"
                    },
                    new AdminMenu
                    {
                        Title = "文章类别",
                        Url = "~/admin/ArticleType",
                        Icon = "glyphicon-th-list"
                    }
                }
            };
        }




        protected override void InitScript(Func<string, Web.Resource.ResourceHelper> script)
        {
            
        }

        protected override void InitStyle(Func<string, Web.Resource.ResourceHelper> style)
        {
            style("Article").Include("~/Modules/Article/Content/Article.css");
        }
    }
}