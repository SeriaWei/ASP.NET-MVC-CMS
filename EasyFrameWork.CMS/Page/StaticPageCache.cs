using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Newtonsoft.Json;

namespace Easy.Web.CMS.Page
{
    public class StaticPageCacheSetting
    {
        public bool Enable { get; set; }
        public int CacheHours { get; set; }
    }
    public class StaticPageCache : IStaticPageCache
    {
        private const string CacheFolder = "~/CachePages/{0}";
        private const string NameFormat = "{0}.page";
        private const string SettingFile = "setting.config";
        private string GetFileName(PageEntity page, HttpRequestBase request)
        {
            if (page.Url.ToLower().IndexOf(request.Url.AbsolutePath.ToLower()) >= 0)
            {
                return NameFormat.FormatWith(page.ReferencePageID);
            }
            var extens = request.Url.AbsolutePath.Replace(page.Url.Replace("~/", "/"), "");
            return NameFormat.FormatWith(page.ReferencePageID + extens.Replace("/", "."));
        }
        private string GetFolder()
        {
            var request = HttpContext.Current.Request;
            return request.MapPath(CacheFolder.FormatWith(request.Url.Host));
        }
        public void Finish(WebViewPage page)
        {
            if (GetSetting().Enable && !page.Request.IsAuthenticated)
            {
                var layout = page.Model as Layout.LayoutEntity;
                if (layout.Page.IsStaticCache ?? false)
                {
                    var html = page.Output.ToString();
                    string fileName = GetFileName(layout.Page, page.Request);
                    var folder = GetFolder();
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    File.WriteAllText(Path.Combine(folder, fileName), html);
                }
            }
        }

        public void Clear()
        {
            Directory.Delete(HttpContext.Current.Request.MapPath(CacheFolder.FormatWith(HttpContext.Current.Request.Url.Host)), true);
        }

        public void Delete(string searchPattern)
        {
            var dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(CacheFolder.FormatWith(HttpContext.Current.Request.Url.Host)));
            dir.GetFiles("*{0}*".FormatWith(searchPattern)).Each(file =>
            {
                file.Delete();
            });
        }


        public string Get(PageEntity page, HttpRequestBase request)
        {
            if (GetSetting().Enable && (page.IsStaticCache ?? false) && !request.IsAuthenticated)
            {
                string file = Path.Combine(GetFolder(), GetFileName(page, request));
                if (File.Exists(file))
                {
                    return File.ReadAllText(file);
                }
            }
            return null;
        }

        public StaticPageCacheSetting GetSetting()
        {
            var setting = Path.Combine(GetFolder(), SettingFile);
            if (File.Exists(setting))
            {
                return JsonConvert.DeserializeObject<StaticPageCacheSetting>(File.ReadAllText(setting));
            }
            return new StaticPageCacheSetting
            {
                Enable = false,
                CacheHours = 24
            };
        }

        public void SaveSetting(StaticPageCacheSetting setting)
        {
            var file = Path.Combine(GetFolder(), SettingFile);
            File.WriteAllText(file, JsonConvert.SerializeObject(setting));
        }
    }
}
