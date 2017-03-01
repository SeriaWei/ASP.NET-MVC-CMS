/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Message
{
    public class MessagePlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            yield return new RouteDescriptor()
            {
                RouteName = "MessagePlug",
                Url = "Message-Handle/{action}",
                Defaults = new { controller = "MessageHandle" },
                Namespaces = new string[] { "Easy.CMS.Message.Controllers" },
                Priority = 11
            };
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "¡Ù—‘∞Â",
                Icon = "glyphicon-volume-up",
                Url = "~/Admin/Message",
                Order = 7
            };
        }

        protected override void InitScript(Func<string, Web.Resource.ResourceHelper> script)
        {
            
        }

        protected override void InitStyle(Func<string, Web.Resource.ResourceHelper> style)
        {
            
        }

        public override IEnumerable<PermissionDescriptor> RegistPermission()
        {
            return null;
        }
    }
}