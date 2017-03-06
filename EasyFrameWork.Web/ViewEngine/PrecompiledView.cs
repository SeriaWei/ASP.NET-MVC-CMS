using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Web.WebPages;
using Easy.Extend;
using System.Web;

namespace Easy.Web.ViewEngine
{
    public class PrecompiledView : IView
    {
        private readonly string[] _viewsExtension = new[] { "cshtml" };
        public PrecompiledView(string virtualPath, string layoutPath, bool partialView, Type viewType)
        {
            VirtualPath = virtualPath;
            LayoutPath = layoutPath;
            PartialView = partialView;
            ViewType = viewType;
        }
        public string VirtualPath { get; set; }
        public bool PartialView { get; set; }
        public Type ViewType { get; set; }
        public string LayoutPath { get; set; }
        public void Render(ViewContext viewContext, TextWriter writer)
        {
            WebViewPage webViewPage = new PrecompiledPageActivator().Create(viewContext.Controller.ControllerContext, ViewType) as WebViewPage;

            if (webViewPage == null)
            {
                throw new InvalidOperationException("Invalid view type");
            }

            webViewPage.Layout = LayoutPath;
            webViewPage.VirtualPath = VirtualPath;
            webViewPage.ViewContext = viewContext;
            webViewPage.ViewData = viewContext.ViewData;
            webViewPage.InitHelpers();

            WebPageRenderingBase startPage = null;
            if (!this.PartialView)
            {
                startPage = StartPage.GetStartPage(webViewPage, "_ViewStart", _viewsExtension);
            }
            var pageContext = new WebPageContext(viewContext.HttpContext, webViewPage, null);
            webViewPage.ExecutePageHierarchy(pageContext, writer, startPage);
        }
    }
}
