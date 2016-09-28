/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Web.Mvc;
using Easy.Web.CMS;
using Easy.Web.Resource;
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
                Namespaces = new[] { "Easy.CMS.Common.Controllers" },
                Priority = -1
                // Constraints = new { path = new RouteConstraint() }
            };
            yield return new RouteDescriptor
            {
                RouteName = "admin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "Dashboard", action = "index", module = "admin" },
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
                Defaults = new { controller = "Error", action = "index", module = "admin" },
                Priority = 10
            };
            yield return new RouteDescriptor
            {
                RouteName = "AccountAdmin",
                Url = "Account/{action}",
                Defaults = new { controller = "Account", action = "Login", module = "common" },
                Namespaces = new[] { "Easy.CMS.Common.Controllers" },
                Priority = 10
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "仪表盘",
                Icon = "glyphicon-dashboard",
                Url = "~/admin",
                Order = 0
            };
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
                        Icon = "glyphicon-align-justify",
                        PermissionKey = PermissionKeys.ViewLayout
                    },
                    new AdminMenu
                    {
                        Title = "布局组件",
                        Url = "~/admin/Layout/LayoutWidget",
                        Icon = "glyphicon-th-list",
                        PermissionKey = PermissionKeys.ViewLayout
                    }
                }
            };
            yield return new AdminMenu
            {
                Title = "页面",
                Icon = "glyphicon-eye-open",
                Url = "~/admin/Page",
                Order = 2,
                PermissionKey = PermissionKeys.ViewPage
            };
            yield return new AdminMenu
            {
                Title = "导航",
                Icon = "glyphicon-retweet",
                Url = "~/admin/Navigation",
                Order = 3,
                PermissionKey = PermissionKeys.ViewNavigation
            };
            yield return new AdminMenu
            {
                Title = "主题",
                Icon = "glyphicon-blackboard",
                Url = "~/admin/Theme",
                Order = 4,
                PermissionKey = PermissionKeys.ViewTheme
            };
            yield return new AdminMenu
            {
                Title = "媒体库",
                Icon = "glyphicon-picture",
                Url = "~/admin/Media",
                Order = 5,
                PermissionKey = PermissionKeys.ViewMedia
            };
            yield return new AdminMenu
            {
                Title = "焦点图",
                Icon = "glyphicon-eye-open",
                Url = "~/admin/Carousel",
                Order = 6,
                PermissionKey = PermissionKeys.ViewCarousel
            };
            yield return new AdminMenu
            {
                Title = "系统",
                Icon = "glyphicon-cog",
                Order = 1000,
                Children = new List<AdminMenu>
                {
                    new AdminMenu
                    {
                        Title = "用户",
                        Icon = "glyphicon-user",
                        Url = "~/admin/User",
                        Order = 1,
                        PermissionKey = PermissionKeys.ViewUser
                    },
                    new AdminMenu
                    {
                        Title = "角色",
                        Icon = "glyphicon-eye-open",
                        Url = "~/admin/Roles",
                        Order = 2,
                        PermissionKey = PermissionKeys.ViewRole
                    },
                    new AdminMenu
                    {
                        Title = "系统设置",
                        Icon = "glyphicon-cog",
                        Url = "~/admin/ApplicationSetting",
                        Order = 3,
                        PermissionKey = PermissionKeys.ViewApplicationSetting
                    }
                }
            };
        }

        protected override void InitScript(Func<string, ResourceHelper> script)
        {
            script("OWL.Carousel").Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js", "~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js", Urls.CdnHost + "/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js")
                .Include("~/Modules/Common/Scripts/Owl.Carousel.js", "~/Modules/Common/Scripts/Owl.Carousel.min.js", Urls.CdnHost + "/Modules/Common/Scripts/Owl.Carousel.min.js");

            script("LayoutDesign").Include("~/Modules/Common/Scripts/LayoutDesign.js", "~/Modules/Common/Scripts/LayoutDesign.min.js", Urls.CdnHost + "/Modules/Common/Scripts/LayoutDesign.min.js");
            script("PageDesign").Include("~/Modules/Common/Scripts/PageDesign.js", "~/Modules/Common/Scripts/PageDesign.min.js", Urls.CdnHost + "/Modules/Common/Scripts/PageDesign.min.js");
        }

        protected override void InitStyle(Func<string, ResourceHelper> style)
        {
            style("Layout").Include("~/Modules/Common/Content/Layout.css", "~/Modules/Common/Content/Layout.min.css", Urls.CdnHost + "/Modules/Common/Content/Layout.min.css");

            style("Login").Include("~/Modules/Common/Content/Login.css", "~/Modules/Common/Content/Login.min.css", Urls.CdnHost + "/Modules/Common/Content/Login.min.css");

            style("OWL.Carousel")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.css", "~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.css", Urls.CdnHost + "/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.css")
                .Include("~/Modules/Common/Scripts/OwlCarousel/owl.transitions.css", "~/Modules/Common/Scripts/OwlCarousel/owl.transitions.min.css", Urls.CdnHost + "/Modules/Common/Scripts/OwlCarousel/owl.transitions.min.css");
        }

        public override IEnumerable<PermissionDescriptor> RegistPermission()
        {
            yield return new PermissionDescriptor(PermissionKeys.ViewPage, "页面", "查看页面", "");
            yield return new PermissionDescriptor(PermissionKeys.ManagePage, "页面", "管理页面", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewLayout, "布局", "查看布局", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageLayout, "布局", "管理布局", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewNavigation, "导航", "查看导航", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageNavigation, "导航", "管理导航", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewTheme, "主题", "查看主题", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageTheme, "主题", "管理主题", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewMedia, "媒体库", "查看媒体库", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageMedia, "媒体库", "管理媒体库", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewCarousel, "焦点图", "查看焦点图", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageCarousel, "焦点图", "管理焦点图", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewUser, "用户/安全", "查看用户", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageUser, "用户/安全", "管理用户", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewRole, "用户/安全", "查看角色", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageRole, "用户/安全", "管理角色", "");

            yield return new PermissionDescriptor(PermissionKeys.ViewApplicationSetting, "用户/安全", "查看系统设置", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageApplicationSetting, "用户/安全", "管理系统设置", "");
        }
    }
}