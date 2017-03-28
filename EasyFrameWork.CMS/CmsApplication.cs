/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Compilation;
using System.Web.Mvc;
using System.Web.Routing;
using Easy.Extend;
using Easy.IOC;
using Easy.Modules.DataDictionary;
using Easy.Modules.MutiLanguage;
using Easy.Modules.User.Service;
using Easy.Security;
using Easy.Web.Application;
using Easy.Web.CMS.ModelBinder;
using Easy.Web.CMS.Widget;
using Easy.Web.Route;
using Easy.Web.ViewEngine;
using System.Web.WebPages;

namespace Easy.Web.CMS
{
    public class CmsApplication : UnityMvcApplication
    {
        public override void Application_Starting()
        {
            ModelBinders.Binders.Add(typeof(WidgetBase), new WidgetBinder());

            var routes = new List<RouteDescriptor>();
            Type plugBaseType = typeof(PluginBase);
            Type widgetModelType = typeof(WidgetBase);
            var types = BuildManager.GetReferencedAssemblies().Cast<Assembly>().SelectMany(assembly => assembly.GetTypes()).ToArray();
            types.Each(p =>
            {
                if (plugBaseType.IsAssignableFrom(p) && !p.IsAbstract && !p.IsInterface)
                {
                    var plug = Activator.CreateInstance(p) as PluginBase;
                    if (plug != null)
                    {
                        var moduleRoutes = plug.RegistRoute();
                        if (moduleRoutes != null)
                        {
                            var routeArray = moduleRoutes.ToArray();
                            if (routeArray.Length > 0)
                            {
                                routes.AddRange(routeArray);
                            }
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
            });
            PrecompliedViewEngine.Regist(types);
            RouteTable.Routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.OrderByDescending(m => m.Priority).Each(m => RouteTable.Routes.MapRoute(m.RouteName, m.Url, m.Defaults, m.Constraints, m.Namespaces));
            ContainerAdapter.RegisterType<IUserService, UserService>();
            ContainerAdapter.RegisterType<IDataDictionaryService, DataDictionaryService>();
            ContainerAdapter.RegisterType<ILanguageService, LanguageService>();
            ContainerAdapter.RegisterType<IAuthorizer, DefaultAuthorizer>();
            ContainerAdapter.RegisterType<IApplicationContext, CMSApplicationContext>(DependencyLifeTime.PerRequest);
            ContainerAdapter.RegisterType<Page.IStaticPageCache, Page.DataBasePageCache>();
            //DisplayViewSupport.SupportMobileView();
            //DisplayViewSupport.SupportIEView();
        }
    }
}