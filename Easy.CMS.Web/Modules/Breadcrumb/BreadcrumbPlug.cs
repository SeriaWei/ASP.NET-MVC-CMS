/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using Easy.Web.CMS;
using Easy.Web.Resource;
using Easy.Web.Route;

namespace Easy.CMS.Breadcrumb
{
    public class BreadcrumbPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            return null;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            return null;
        }

        protected override void InitScript(Func<string, ResourceHelper> script)
        {
            
        }

        protected override void InitStyle(Func<string, ResourceHelper> style)
        {
            
        }

        public override IEnumerable<PermissionDescriptor> RegistPermission()
        {
            return null;
        }
    }
}