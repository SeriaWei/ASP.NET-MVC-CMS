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
                Title = "图片",
                Icon = "glyphicon glyphicon-picture",
                Url = "~/admin/Image",
                Order = 4
            };
            yield return new AdminMenu
            {
                Title = "焦点图",
                Icon = "glyphicon glyphicon-eye-open",
                Url = "~/admin/Carousel",
                Order = 5
            };
            yield return new AdminMenu
            {
                Title = "用户",
                Icon = "glyphicon-user",
                Url = "~/admin/User",
                Order = 100,
            };
        }

        public override void InitScript()
        {
            Script("OWL.Carousel").Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js")
                .Include("~/Modules/Common/Scripts/Owl.Carousel.js", "~/Modules/Common/Scripts/Owl.Carousel.min.js");

            Script("LayoutDesign").Include("~/Modules/Common/Scripts/LayoutDesign.js");
            Script("PageDesign").Include("~/Modules/Common/Scripts/PageDesign.js");
        }

        public override void InitStyle()
        {
            Style("Layout").Include("~/Modules/Common/Content/Layout.css");
            Style("OWL.Carousel")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.css", "~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.css")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.transitions.css", "~/Modules/Common/Scripts/OwlCarousel/owl.transitions.min.css");
        }
    }
}