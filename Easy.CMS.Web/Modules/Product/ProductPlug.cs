using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.Web.CMS;
using Easy.Web.Route;

namespace Easy.CMS.Product
{
    public class ProductPlug : PluginBase
    {
        public override IEnumerable<RouteDescriptor> RegistRoute()
        {
            return null;
        }

        public override IEnumerable<AdminMenu> AdminMenu()
        {
            yield return new AdminMenu
            {
                Title = "产品管理",
                Icon = "glyphicon-list-alt",
                Order = 11,
                Children = new List<AdminMenu>
                {
                    new AdminMenu
                    {
                        Title = "产品列表",
                        Url = "~/admin/Product",
                        Icon = "glyphicon-align-justify",
                        PermissionKey = PermissionKeys.ViewProduct
                    },
                    new AdminMenu
                    {
                        Title = "产品类别",
                        Url = "~/admin/ProductCategory",
                        Icon = "glyphicon-th-list",
                        PermissionKey = PermissionKeys.ViewProductCategory
                    }
                }
            };
        }

        protected override void InitScript(Func<string, Web.Resource.ResourceHelper> script)
        {
            script("PhotoWall")
                .Include("~/Modules/Product/Scripts/jquery-photowall/jquery-photowall.js");
        }

        protected override void InitStyle(Func<string, Web.Resource.ResourceHelper> style)
        {
            style("PhotoWall")
                .Include("~/Modules/Product/Scripts/jquery-photowall/jquery-photowall.css");
        }

        public override IEnumerable<PermissionDescriptor> RegistPermission()
        {
            yield return new PermissionDescriptor(PermissionKeys.ViewProduct, "产品", "查看产品", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageProduct, "产品", "管理产品", "");
            yield return new PermissionDescriptor(PermissionKeys.ViewProductCategory, "产品", "查看产品类别", "");
            yield return new PermissionDescriptor(PermissionKeys.ManageProductCategory, "产品", "管理产品类别", "");
        }
    }
}