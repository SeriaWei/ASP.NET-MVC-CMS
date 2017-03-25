using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;
using Newtonsoft.Json;
using Easy.Web.CMS.DataArchived;

namespace Easy.Web.CMS.Page
{
    public class DataBasePageCache : IStaticPageCache
    {
        private readonly IDataArchivedService _dataArchivedService;
        private const string NameFormat = "PageHtmlContent:{0}.page";
        private const string SettingKey = "PageHtmlContent.Setting";
        public DataBasePageCache(IDataArchivedService dataArchivedService)
        {
            _dataArchivedService = dataArchivedService;
        }
        private string GetCacheKey(PageEntity page, HttpRequestBase request)
        {
            if (page.Url.ToLower().IndexOf(request.Url.AbsolutePath.ToLower()) >= 0)
            {
                return NameFormat.FormatWith(page.ReferencePageID);
            }
            var extens = request.Url.AbsolutePath.Replace(page.Url.Replace("~/", "/"), "");
            return NameFormat.FormatWith(page.ReferencePageID + extens.Replace("/", "."));
        }

        public void Finish(WebViewPage page)
        {
            if (GetSetting().Enable && !page.Request.IsAuthenticated)
            {
                var layout = page.Model as Layout.LayoutEntity;
                if (layout.Page.IsStaticCache ?? false)
                {
                    var html = page.Output.ToString();
                    string fileName = GetCacheKey(layout.Page, page.Request);

                    _dataArchivedService.Add(new DataArchived.DataArchived { ID = fileName, Data = html });
                }
            }
        }

        public void Clear()
        {
            _dataArchivedService.Delete(new Data.DataFilter().Where("ID", Data.OperatorType.StartWith, "PageHtmlContent:"));
        }

        public void Delete(string searchPattern)
        {
            _dataArchivedService.Delete(new Data.DataFilter().Where("ID", Data.OperatorType.StartWith, "PageHtmlContent:").Where("ID", Data.OperatorType.Contains, searchPattern));
        }


        public string Get(PageEntity page, HttpRequestBase request)
        {
            var setting = GetSetting();
            if (setting.Enable && (page.IsStaticCache ?? false) && !request.IsAuthenticated)
            {
                var data = _dataArchivedService.Get(GetCacheKey(page, request));

                if (data != null && (setting.CacheHours == 0 || data.CreateDate.Value.AddHours(setting.CacheHours) > DateTime.Now))
                {
                    return data.Data;
                }
            }
            return null;
        }

        public StaticPageCacheSetting GetSetting()
        {
            var setting = _dataArchivedService.Get(SettingKey);
            if (setting != null)
            {
                return JsonConvert.DeserializeObject<StaticPageCacheSetting>(setting.Data);
            }
            return new StaticPageCacheSetting
            {
                Enable = false,
                CacheHours = 24
            };
        }

        public void SaveSetting(StaticPageCacheSetting setting)
        {
            _dataArchivedService.Add(new DataArchived.DataArchived { ID = SettingKey, Data = JsonConvert.SerializeObject(setting) });
        }
    }
}
