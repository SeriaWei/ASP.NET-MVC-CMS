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
                RouteName = "layoutAdmin",
                Url = "admin/{controller}/{action}",
                Defaults = new { controller = "layout", action = "index", module = "common" },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
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
            yield return new RouteDescriptor
            {
                RouteName = "UserAdmin",
                Url = "User/{action}",
                Defaults = new { controller = "User", action = "Index", module = "common" },
                Namespaces = new string[] { "Easy.CMS.Common.Controllers" },
                Priority = 10
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            throw new NotImplementedException();
        }

        public override void InitScript()
        {
            Script("OWL.Carousel").Include("~/Modules/Common/Scripts/OwlCarousel/owl.carousel.min.js")
                .Include("~/Modules/Common/Scripts/Owl.Carousel.js", "~/Modules/Common/Scripts/Owl.Carousel.min.js");
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