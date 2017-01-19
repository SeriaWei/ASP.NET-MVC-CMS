using Easy.Web.CMS.Page;
/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Web;

namespace Easy.Web.CMS
{
    public class CMSApplicationContext : ApplicationContext
    {
        private Uri _requestUrl;
        public Uri RequestUrl
        {
            get
            {
                if (_requestUrl == null)
                {
                    if (HttpContext.Current != null)
                    {
                        _requestUrl = HttpContext.Current.Request.Url;
                    }
                }
                return _requestUrl;
            }
            set { _requestUrl = new Uri(value.AbsoluteUri); }
        }
        public string MapPath(string path)
        {
            if (HttpContext.Current != null)
            {
               return HttpContext.Current.Server.MapPath(path);
            }
            return path;
        }

        public PageViewMode ViewMode { get; set; }

    }
}
