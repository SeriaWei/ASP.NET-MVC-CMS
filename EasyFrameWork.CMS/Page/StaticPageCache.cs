using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.CMS.Page
{
    public class StaticPageCache : IStaticPageCache
    {
        private const string CacheFolder = "~/CachePages/{0}";
        private const string NameFormat = "{0}.page";
        private string GetFileName(PageEntity page, HttpRequestBase request)
        {
            if (page.Url.ToLower().IndexOf(request.Url.AbsolutePath.ToLower()) >= 0)
            {
                return NameFormat.FormatWith(page.ReferencePageID);
            }
            var extens = request.Url.AbsolutePath.Replace(page.Url.Replace("~/", "/"), "");
            return NameFormat.FormatWith(page.ReferencePageID + extens.Replace("/", "."));
        }
        private string GetFolder(HttpRequestBase request)
        {
            return request.MapPath(CacheFolder.FormatWith(request.Url.Host));
        }
        public void Finish(WebViewPage page)
        {
            if (!page.Request.IsAuthenticated)
            {
                var layout = page.Model as Layout.LayoutEntity;
                var html = page.Output.ToString();
                string fileName = GetFileName(layout.Page, page.Request);
                var folder = GetFolder(page.Request);
                if (!Directory.Exists(folder))
                {
                    Directory.CreateDirectory(folder);
                }
                File.WriteAllText(Path.Combine(folder, fileName), html);
            }            
        }

        public void Clear()
        {

        }

        public void Delete(string key)
        {
            
        }


        public string Get(PageEntity page, HttpRequestBase request)
        {
            if (!request.IsAuthenticated)
            {
                string file = Path.Combine(GetFolder(request), GetFileName(page, request));
                if (File.Exists(file))
                {
                    return File.ReadAllText(file);
                }
            }
            return null;
        }
    }
}
