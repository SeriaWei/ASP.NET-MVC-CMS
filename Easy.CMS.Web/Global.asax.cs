using System.Web.Mvc;
using Easy.Web.CMS;

namespace Easy.CMS.Web
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : CmsApplication
    {
        public override void Application_Starting()
        {
            base.Application_Starting();
            //AreaRegistration.RegisterAllAreas();

            //WebApiConfig.Register(GlobalConfiguration.Configuration);
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);
            //AuthConfig.RegisterAuth();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            new ResourceManager().Excute();
        }

    }
}