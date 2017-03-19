using Easy.Web.CMS.PackageManger;
using Microsoft.Practices.ServiceLocation;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Hosting;

namespace Easy.Web.CMS.Theme
{
    public class ThemePackageInstaller : FilePackageInstaller
    {
        private const string ThemePath = "~/Themes";
        public override string PackageInstaller
        {
            get
            {
                return "ThemePackageInstaller";
            }
        }
        public override object Install(Package package)
        {
            base.Install(package);
            var themePackage = package as ThemePackage;
            if (themePackage != null)
            {
                var newTheme = themePackage.Theme;
                newTheme.IsActived = false;
                var themeService = ServiceLocator.Current.GetInstance<IThemeService>();
                if (themeService.Count(m => m.ID == newTheme.ID) == 0)
                {
                    themeService.Add(newTheme);
                }
                else
                {
                    var oldTheme = themeService.Get(newTheme.ID);
                    if (oldTheme.IsActived)
                    {
                        newTheme.IsActived = true;
                    }
                    themeService.Update(newTheme);
                }
            }
            return package;
        }
        public override Package Pack(object obj)
        {
            //obj is theme id
            var themeService = ServiceLocator.Current.GetInstance<IThemeService>();
            string path = HostingEnvironment.MapPath(ThemePath + "/" + obj);
            if (Directory.Exists(path))
            {
                var theme = themeService.Get(obj.ToString());
                var package = base.Pack(new DirectoryInfo(path));
                (package as ThemePackage).Theme = theme;
                return package;
            }
            return null;
        }
        public override FilePackage CreatePackage()
        {
            return new ThemePackage(PackageInstaller);
        }
    }
}
