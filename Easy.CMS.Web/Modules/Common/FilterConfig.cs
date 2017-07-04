/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.CMS.Common.Controllers;
using Easy.CMS.Common.Models;
using Easy.Modules.User.Models;
using Easy.Web.Authorize;
using Easy.Web.CMS.Layout;
using Easy.Web.CMS.Media;
using Easy.Web.CMS.Page;
using Easy.Web.CMS.Setting;
using Easy.Web.CMS.Theme;
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
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Create(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Create(new PageEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Edit(new PageEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.GetPageTree(), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Design(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.RedirectView(string.Empty, null), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Select(), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.PageZones(null), auth => auth.SetPermissionKey(PermissionKeys.ViewPage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.MovePage(string.Empty, 0, 0), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.Publish(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));
            Registry.Register<PageController, DefaultAuthorizeAttribute>(m => m.PublishPage(string.Empty, string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManagePage));

            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewCarousel));
            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Edit(0), auth => auth.SetPermissionKey(PermissionKeys.ManageCarousel));
            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Edit(null), auth => auth.SetPermissionKey(PermissionKeys.ManageCarousel));
            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageCarousel));
            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageCarousel));
            Registry.Register<CarouselController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageCarousel));

            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Edit(new LayoutEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.LayoutWidget(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ViewLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.LayoutZones(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ViewLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.Design(string.Empty, string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.SaveLayout(null, null, null), auth => auth.SetPermissionKey(PermissionKeys.ManageLayout));
            Registry.Register<LayoutController, DefaultAuthorizeAttribute>(m => m.SelectZone(null, null, null), auth => auth.SetPermissionKey(PermissionKeys.ViewLayout));

            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Index(string.Empty, null), auth => auth.SetPermissionKey(PermissionKeys.ViewMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Select(string.Empty, null), auth => auth.SetPermissionKey(PermissionKeys.ViewMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Edit(new MediaEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Save(string.Empty, string.Empty, string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Upload(string.Empty, string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));
            Registry.Register<MediaController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageMedia));

            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.GetNavTree(), auth => auth.SetPermissionKey(PermissionKeys.ViewNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Edit(new NavigationEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Create(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Create(new NavigationEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));
            Registry.Register<NavigationController, DefaultAuthorizeAttribute>(m => m.MoveNav(string.Empty, string.Empty, 0, 0), auth => auth.SetPermissionKey(PermissionKeys.ManageNavigation));

            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.PreView(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ViewTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.CancelPreView(), auth => auth.SetPermissionKey(PermissionKeys.ViewTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Edit(new ThemeEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.ChangeTheme(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.ThemePackage(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.UploadTheme(), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));
            Registry.Register<ThemeController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageTheme));

            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewUser));
            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageUser));
            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Edit(new UserEntity()), auth => auth.SetPermissionKey(PermissionKeys.ManageUser));
            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageUser));
            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Create(null), auth => auth.SetPermissionKey(PermissionKeys.ManageUser));
            Registry.Register<UserController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageUser));

            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewRole));
            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageRole));
            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Edit(null, null), auth => auth.SetPermissionKey(PermissionKeys.ManageRole));
            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageRole));
            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Create(null, null), auth => auth.SetPermissionKey(PermissionKeys.ManageRole));
            Registry.Register<RolesController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageRole));

            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Index(), auth => auth.SetPermissionKey(PermissionKeys.ViewApplicationSetting));
            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Edit(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageApplicationSetting));
            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Edit(new ApplicationSetting()), auth => auth.SetPermissionKey(PermissionKeys.ManageApplicationSetting));
            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Create(), auth => auth.SetPermissionKey(PermissionKeys.ManageApplicationSetting));
            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Create(new ApplicationSetting()), auth => auth.SetPermissionKey(PermissionKeys.ManageApplicationSetting));
            Registry.Register<ApplicationSettingController, DefaultAuthorizeAttribute>(m => m.Delete(string.Empty), auth => auth.SetPermissionKey(PermissionKeys.ManageApplicationSetting));
        }

    }
}