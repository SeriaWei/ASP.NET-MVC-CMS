using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Common
{
    public class CommonPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor
            {
                RouteName = "pageRoute",
                Url = "{*path}",
                Defaults = new { controller = "Page", action = "PreView", path = UrlParameter.Optional },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
                Priority = -1,
                // Constraints = new { path = new RouteConstraint() }
            };
            yield return new RouteDescriptor
            {
                RouteName = "admin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "page", action = "index", module = "admin" },
                Priority = 10
            };
            yield return new RouteDescriptor
            {
                RouteName = "Validation",
                Url = "validation/{action}",
                Defaults = new { controller = "Validation", module = "validation" },
                Priority = 10
            };
            yield return new RouteDescriptor
            {
                RouteName = "error",
                Url = "error/{action}",
                Defaults = new { controller = "Error",  action="index",module = "admin" },
                Priority = 10
            };
            yield return new RouteDescriptor
            {
                RouteName = "AccountAdmin",
                Url = "Account/{action}",
                Defaults = new { controller = "Account", action = "Login", module = "common" },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
                Priority = 10
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "布局",
                Icon = "glyphicon-th-list",
                Order = 1,
                Children = new List<AdminMenu>
                {
                    new AdminMenu
                    {
                        Title = "布局列表",
                        Url = "~/admin/Layout",
                        Icon = "glyphicon-align-justify"
                    },
                    new AdminMenu
                    {
                        Title = "布局组件",
                        Url = "~/admin/Layout/LayoutWidget",
                        Icon = "glyphicon-th-list"
                    }
                }
            };
            yield return new AdminMenu
            {
                Title = "页面",
                Icon = "glyphicon-eye-open",
                Url = "~/admin",
                Order = 2
            };
            yield return new AdminMenu
            {
                Title = "导航",
                Icon = "glyphicon-retweet",
                Url = "~/admin/Navigation",
                Order = 3
            };
            yield return new AdminMenu
            {
                Title = "主题",
                Icon = "glyphicon-blackboard",
                Url = "~/admin/Theme",
                Order = 4
            };
            yield return new AdminMenu
            {
                Title = "媒体库",
                Icon = "glyphicon glyphicon-picture",
                Url = "~/admin/Media",
                Order = 5
            };
            yield return new AdminMenu
            {
                Title = "焦点图",
                Icon = "glyphicon glyphicon-eye-open",
                Url = "~/admin/Carousel",
                Order = 6
            };
            yield return new AdminMenu
            {
                Title = "用户",
                Icon = "glyphicon-user",
                Url = "~/admin/User",
                Order = 100,
            };
        }

        protected override void InitScript(Func<string, Web.Resource.ResourceHelper> script)
        {
            script("OWL.Carousel").Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js")
                .Include("~/Modules/Common/Scripts/Owl.Carousel.js", "~/Modules/Common/Scripts/Owl.Carousel.min.js");

            script("LayoutDesign").Include("~/Modules/Common/Scripts/LayoutDesign.js", "~/Modules/Common/Scripts/LayoutDesign.min.js");
            script("PageDesign").Include("~/Modules/Common/Scripts/PageDesign.js", "~/Modules/Common/Scripts/PageDesign.min.js");
        }

        protected override void InitStyle(Func<string, Web.Resource.ResourceHelper> style)
        {
            style("Layout").Include("~/Modules/Common/Content/Layout.css", "~/Modules/Common/Content/Layout.min.css");
            style("OWL.Carousel")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.css", "~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.css")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.transitions.css", "~/Modules/Common/Scripts/OwlCarousel/owl.transitions.min.css");
        }
    }
}