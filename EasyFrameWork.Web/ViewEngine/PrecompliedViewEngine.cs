/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using Easy.Extend;

namespace Easy.Web.ViewEngine
{
    /// <summary>
    /// ViewEngines.Engines.Add(engine);
    /// </summary>
    public class PrecompliedViewEngine : RazorViewEngine, IVirtualPathFactory
    {
        private const string ViewsFolder = "Views";
        private const string SharedFolder = "Shared";
        private const string AreasFolder = "Areas";
        private readonly string[] _viewsExtension = new[] { ".cshtml" };

        private const string NormalViewPathFormat = "~/{0}/{1}/{{0}}{2}";
        private const string ModuleNormalViewPathFormat = "~/{3}/{4}/{0}/{1}/{{0}}{2}";
        private const string ModuleWidgetViewPathFormat = "~/{2}/{3}/{0}/{{0}}{1}";

        private const string NormalAreasViewPathFormat = "~/{0}/{{2}}/{1}/{2}/{{0}}{3}";
        private const string ModuleNormalAreasViewPathFormat = "~/{4}/{5}/{0}/{{2}}/{1}/{2}/{{0}}{3}";

        private static Dictionary<string, Type> PrecompliedViewTypes = new Dictionary<string, Type>();
        private static Type WebPageType = typeof(WebPageBase);

        public PrecompliedViewEngine(string moduleFolder = "Modules")
        {
            List<string> areaViewPathList = new List<string>();
            List<string> viewPathList = new List<string>();
            foreach (string ext in _viewsExtension)
            {
                areaViewPathList.Add(string.Format(NormalAreasViewPathFormat, AreasFolder, ViewsFolder, "{1}", ext));
                areaViewPathList.Add(string.Format(NormalAreasViewPathFormat, AreasFolder, ViewsFolder, SharedFolder, ext));

                viewPathList.Add(string.Format(NormalViewPathFormat, ViewsFolder, "{1}", ext));
                viewPathList.Add(string.Format(NormalViewPathFormat, ViewsFolder, SharedFolder, ext));
            }
            string dir = AppDomain.CurrentDomain.BaseDirectory;
            dir += moduleFolder;
            DirectoryInfo dirInfo = new DirectoryInfo(dir);
            if (dirInfo.Exists)
            {
                foreach (DirectoryInfo item in dirInfo.GetDirectories())
                {
                    foreach (string ext in _viewsExtension)
                    {
                        areaViewPathList.Add(string.Format(ModuleNormalAreasViewPathFormat, AreasFolder, ViewsFolder, "{1}", ext, moduleFolder, item.Name));
                        areaViewPathList.Add(string.Format(ModuleNormalAreasViewPathFormat, AreasFolder, ViewsFolder, SharedFolder, ext, moduleFolder, item.Name));

                        viewPathList.Add(string.Format(ModuleNormalViewPathFormat, ViewsFolder, "{1}", ext, moduleFolder, item.Name));
                        viewPathList.Add(string.Format(ModuleNormalViewPathFormat, ViewsFolder, SharedFolder, ext, moduleFolder, item.Name));

                        viewPathList.Add(string.Format(ModuleWidgetViewPathFormat, ViewsFolder, ext, moduleFolder, item.Name));
                    }
                }
            }
            //init other format in list


            //area
            AreaViewLocationFormats = AreaPartialViewLocationFormats = AreaMasterLocationFormats = areaViewPathList.ToArray();

            //normal
            ViewLocationFormats = PartialViewLocationFormats = MasterLocationFormats = viewPathList.ToArray();
        }

        public static void Regist(IEnumerable<Type> types, string moduleFolder = "Modules")
        {
            if (types != null)
            {
                types.Where(type => WebPageType.IsAssignableFrom(type)).Each(type =>
                {
                    var virtualPathAttrs = type.GetCustomAttributes(typeof(PageVirtualPathAttribute), false);
                    if (virtualPathAttrs.Length > 0)
                    {
                        lock (PrecompliedViewTypes)
                        {
                            string virtualPath = ((PageVirtualPathAttribute)virtualPathAttrs[0]).VirtualPath.ToUpper();
                            if (!PrecompliedViewTypes.ContainsKey(virtualPath))
                            {
                                PrecompliedViewTypes.Add(virtualPath, type);
                            }
                        }

                    }
                });
            }
            var engine = new PrecompliedViewEngine(moduleFolder);
            ViewEngines.Engines.Insert(0, engine);
            VirtualPathFactoryManager.RegisterVirtualPathFactory(engine);
        }
        public static Type GetViewType(string virtualPath)
        {
            if (virtualPath.IsNullOrWhiteSpace())
            {
                return null;
            }
            virtualPath = virtualPath.ToUpper();
            if (PrecompliedViewTypes.ContainsKey(virtualPath))
            {
                return PrecompliedViewTypes[virtualPath];
            }
            return null;
        }

        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            var preCompiledViewType = GetViewType(partialPath);
            if (preCompiledViewType != null)
            {
                return new PrecompiledView(partialPath, null, true, preCompiledViewType);
            }
            return base.CreatePartialView(controllerContext, partialPath);

        }
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            var preCompiledViewType = GetViewType(viewPath);
            if (preCompiledViewType != null)
            {
                return new PrecompiledView(viewPath, masterPath, false, preCompiledViewType);
            }
            return base.CreateView(controllerContext, viewPath, masterPath);
        }

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            if (GetViewType(virtualPath) != null)
            {
                return true;
            }
            return base.FileExists(controllerContext, virtualPath);
        }
        public override ViewEngineResult FindPartialView(ControllerContext controllerContext, string partialViewName, bool useCache)
        {
            if (controllerContext.RouteData.Values.ContainsKey("module"))
            {
                string virtualPath = string.Format("~/Modules/{0}/Views/{1}.cshtml",
                    controllerContext.RouteData.Values["module"], partialViewName);

                if (!FileExists(controllerContext, virtualPath))
                {
                    virtualPath = string.Format("~/Modules/{0}/Views/{1}/{2}.cshtml",
                     controllerContext.RouteData.Values["module"],
                     controllerContext.RouteData.Values["controller"], partialViewName);
                    if (!FileExists(controllerContext, virtualPath))
                    {
                        return base.FindPartialView(controllerContext, partialViewName, useCache);
                    }
                }
                ViewEngineResult result = new ViewEngineResult(CreatePartialView(controllerContext, virtualPath), this);
                return result;
            }
            return base.FindPartialView(controllerContext, partialViewName, useCache);
        }
        public override ViewEngineResult FindView(ControllerContext controllerContext, string viewName, string masterName, bool useCache)
        {
            if (controllerContext.RouteData.Values.ContainsKey("module"))
            {
                string virtualPath = string.Format("~/Modules/{0}/Views/{1}/{2}.cshtml",
                    controllerContext.RouteData.Values["module"],
                controllerContext.RouteData.Values["controller"], viewName);

                if (FileExists(controllerContext, virtualPath))
                {
                    ViewEngineResult result = new ViewEngineResult(CreateView(controllerContext, virtualPath, masterName), this);
                    return result;
                }
            }
            return base.FindView(controllerContext, viewName, masterName, useCache);
        }
        public override void ReleaseView(ControllerContext controllerContext, IView view)
        {
            base.ReleaseView(controllerContext, view);
        }

        public bool Exists(string virtualPath)
        {
            return GetViewType(virtualPath) != null;
        }

        public object CreateInstance(string virtualPath)
        {
            var viewType = GetViewType(virtualPath);
            if (viewType != null)
            {
                return new PrecompiledPageActivator().Create(null, viewType);
            }
            return null;
        }
    }
}
