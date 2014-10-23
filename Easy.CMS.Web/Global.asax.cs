using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Web;
using System.Web.Compilation;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Easy.Web.CMS;
using Easy.Web.CMS.ModelBinder;
using Easy.Web.CMS.Widget;
using Easy.Extend;
using Easy.Web.MetadataProvider;
using Easy.Web.Route;
using Easy.Modules.DataDictionary;
using Easy.IOCAdapter;
using Easy.Web.ViewEngine;

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
            ViewEngines.Engines.Add(new PlugViewEngine());

            ModelBinders.Binders.Add(typeof(WidgetBase), new WidgetBinder());

            ModelMetadataProviders.Current = new EasyModelMetaDataProvider();

            var reMan = new ResourceManager();
            reMan.InitScript();
            reMan.InitStyle();

            var routes = new List<RouteDescriptor>();
            Type plugBaseType = typeof(PluginBase);

            
            BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(m => m.GetTypes().Each(p =>
            {
                if (plugBaseType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                {
                    var plug = Activator.CreateInstance(p) as PluginBase;
                    if (plug != null)
                    {
                        routes.AddRange(plug.Regist());
                        plug.InitScript();
                        plug.InitStyle();
                    }
                }
            }));
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.OrderByDescending(m => m.Priority).Each(m => RouteTable.Routes.MapRoute(m.RouteName, m.Url, m.Defaults, m.Constraints, m.Namespaces));
            Container.Register(typeof(IDataDictionaryService), typeof(DataDictionaryService));
        }
    }
}