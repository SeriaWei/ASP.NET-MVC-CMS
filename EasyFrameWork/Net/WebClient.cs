/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;

namespace Easy.Net
{
    public class WebClient : System.Net.WebClient
    {
        CookieContainer _cookieContainer;
        public WebClient()
        {
            this._cookieContainer = new CookieContainer();
        }
        public CookieContainer Cookies
        {
            get { return this._cookieContainer; }
            set { this._cookieContainer = value; }
        }
        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                HttpWebRequest httpRequest = request as HttpWebRequest;
                httpRequest.CookieContainer = _cookieContainer;
            }
            return request;
        }
    }
}
