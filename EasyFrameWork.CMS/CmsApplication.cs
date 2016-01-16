using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;
using Easy.Web.Application;
using Easy.Web.CMS.ModelBinder;
using Easy.Web.CMS.Widget;
using Easy.Web.Route;
using Easy.Web.ViewEngine;
using Easy.Extend;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Modules.User.Service;

namespace Easy.Web.CMS
{
    public class CmsApplication : UnityMvcApplication
    {
        public override void Application_Starting()
        {
            ViewEngines.Engines.Clear();
            ViewEngines.Engines.Add(new PlugViewEngine());

            ModelBinders.Binders.Add(typeof(WidgetBase), new WidgetBinder());

            var routes = new List<RouteDescriptor>();
            Type plugBaseType = typeof(PluginBase);
            
            BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(m => m.GetTypes().Each(p =>
            {
                if (plugBaseType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                {
                    var plug = Activator.CreateInstance(p) as PluginBase;
                    if (plug != null)
                    {
                        routes.AddRange(plug.RegistRoute());
                        plug.InitScript();
                        plug.InitStyle();
                    }
                }
            }));
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.OrderByDescending(m => m.Priority).Each(m => RouteTable.Routes.MapRoute(m.RouteName, m.Url, m.Defaults, m.Constraints, m.Namespaces));
            ContainerAdapter.RegisterType<IUserService, UserService>();
            ContainerAdapter.RegisterType<IDataDictionaryService, DataDictionaryService>();
            ContainerAdapter.RegisterType<ILanguageService, LanguageService>();

            //DisplayViewSupport.SupportMobileView();
            //DisplayViewSupport.SupportIEView();
        }
    }
}