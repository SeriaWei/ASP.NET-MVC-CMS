using Easy.IOC;
using Easy.Web.Page;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Easy.Web.CMS.Page
{
    public interface IStaticPageCache : IOnPageFinished
    {
        string Get(PageEntity page, HttpRequestBase request);
        void Clear();
        void Delete(string searchPattern);
        StaticPageCacheSetting GetSetting();
        void SaveSetting(StaticPageCacheSetting setting);
    }
}
