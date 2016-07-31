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
using Easy.IOC;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Modules.User.Service;
using Easy.Web.ValueProvider;
using Easy.Security;

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
            Type widgetModelType = typeof(WidgetBase);
            BuildManager.GetReferencedAssemblies().Cast<Assembly>().Each(m => m.GetTypes().Each(p =>
            {
                if (plugBaseType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                {
                    var plug = Activator.CreateInstance(p) as PluginBase;
                    if (plug != null)
                    {
                        var moduleRoutes = plug.RegistRoute();
                        if (moduleRoutes != null && moduleRoutes.Any())
                        {
                            routes.AddRange(moduleRoutes);
                        }
                        plug.Excute();
                    }
                }
                else if (widgetModelType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface && !WidgetBase.KnownWidgetModel.ContainsKey(p.FullName))
                {
                    lock (WidgetBase.KnownWidgetModel)
                    {
                        WidgetBase.KnownWidgetModel.Add(p.FullName, p);
                    }
                }
            }));
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.OrderByDescending(m => m.Priority).Each(m => RouteTable.Routes.MapRoute(m.RouteName, m.Url, m.Defaults, m.Constraints, m.Namespaces));
            ContainerAdapter.RegisterType<IUserService, UserService>();
            ContainerAdapter.RegisterType<IDataDictionaryService, DataDictionaryService>();
            ContainerAdapter.RegisterType<ILanguageService, LanguageService>();
            ContainerAdapter.RegisterType<IAuthorizer, DefaultAuthorizer>();
            ContainerAdapter.RegisterType<IApplicationContext, CMSApplicationContext>(DependencyLifeTime.PerRequest);

            //DisplayViewSupport.SupportMobileView();
            //DisplayViewSupport.SupportIEView();
        }
    }
}