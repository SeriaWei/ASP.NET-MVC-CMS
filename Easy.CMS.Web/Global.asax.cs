using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Easy.Extend;
using Easy.Web.Route;
using Easy.Modules.DataDictionary;
using Easy.IOCAdapter;

namespace PlugWeb
{
    // 注意: 有关启用 IIS6 或 IIS7 经典模式的说明，
    // 请访问 http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();

            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new Easy.Web.ViewEngine.PlugViewEngine());

            ModelBinders.Binders.Add(typeof(Easy.CMS.Widget.WidgetBase), new Easy.CMS.ModelBinder.WidgetBinder());
            ModelMetadataProviders.Current = new Easy.Web.MetadataProvider.EasyModelMetaDataProvider();
            List<RouteDescriptor> routes = new List<RouteDescriptor>();
            BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(m =>
            {
                m.GetTypes().Each(p =>
                {
                    if (Easy.Web.CommonType.IRouteRegisterType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    {
                        routes.AddRange((Activator.CreateInstance(p) as IRouteRegister).Regist());
                    }
                    else if (Easy.Web.CommonType.ResourceManagerType.IsAssignableFrom(p) && !p.IsInterface && !p.IsAbstract)
                    {
                        var mgr = (Activator.CreateInstance(p) as Easy.Web.Resource.ResourceManager);
                        mgr.InitScript();
                        mgr.InitStyle();
                    }
                });
            });
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.OrderByDescending(m => m.Priority).Each(m =>
            {
                RouteTable.Routes.MapRoute(m.RouteName, m.Url, m.Defaults, m.Constraints, m.Namespaces);
            });
            Container.Register(typeof(IDataDictionaryService), typeof(DataDictionaryService));
        }
    }
}