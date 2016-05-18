using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using Easy.Extend;

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
    }
}
