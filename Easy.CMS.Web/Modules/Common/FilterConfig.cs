using Easy.CMS.Common.Controllers;
using Easy.Web.Authorize;
using Easy.Web.CMS.Page;
using Easy.Web.Filter;
namespace Easy.CMS.Common
{
    public class FilterConfig : ConfigureFilterBase
    {
        public FilterConfig(IFilterRegister register)
            : base(register)
        {
        }
        public override void Configure()
        {
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.GetPageTree(), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Create(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Create(new PageEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Edit(new PageEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Design(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.RedirectView(string.Empty, null), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Select(), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.PageZones(null), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.MovePage(string.Empty, 0, 0), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Publish(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.PublishPage(string.Empty, string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
        }

    }
}