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
    public class FileStaticPageCache : IStaticPageCache
    {
        private const string CacheFolder = "~/CachePages";
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
            var folder= request.MapPath(CacheFolder);
            if (!Directory.Exists(folder))
            {
                Directory.CreateDirectory(folder);
            }
            return folder;
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
                    File.WriteAllText(Path.Combine(GetFolder(), fileName), html);
                }
            }
        }

        public void Clear()
        {
            var dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(CacheFolder.FormatWith(HttpContext.Current.Request.Url.Host)));
            dir.GetFiles(NameFormat.FormatWith("*")).Each(file =>
            {
                file.Delete();
            });
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
            var setting = GetSetting();
            if (setting.Enable && (page.IsStaticCache ?? false) && !request.IsAuthenticated)
            {
                string file = Path.Combine(GetFolder(), GetFileName(page, request));
                FileInfo pageFile = new FileInfo(file);
                if (pageFile.Exists && (setting.CacheHours == 0 || pageFile.LastWriteTime.AddHours(setting.CacheHours) > DateTime.Now))
                {
                    using (StreamReader reader = new StreamReader(pageFile.OpenRead()))
                    {
                        return reader.ReadToEnd();
                    }
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

        public long Count()
        {
            var dir = new DirectoryInfo(HttpContext.Current.Request.MapPath(CacheFolder.FormatWith(HttpContext.Current.Request.Url.Host)));
            if (dir.Exists)
            {
                return dir.GetFiles(NameFormat.FormatWith("*")).Count();
            }
            return 0;
        }
    }
}
