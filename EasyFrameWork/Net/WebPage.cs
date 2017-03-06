/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using Easy.Extend;

namespace Easy.Net
{
    public class WebPage
    {
        public enum Methods
        {
            Get = 1,
            POST = 2
        }
        HttpWebRequest webRequest;
        public WebPage(string Url)
        {
            this.PageUrl = new Uri(Url);
            this.Method = Methods.Get;
            Cookies = new List<Cookie>();
            Header = new Dictionary<string, string>();
        }
        public WebPage(Uri Url)
        {
            this.PageUrl = Url;
            this.Method = Methods.Get;
            Cookies = new List<Cookie>();
            Header = new Dictionary<string, string>();
        }
        public List<Cookie> Cookies { get; set; }
        public Dictionary<string, string> Header { get; set; }
        public Uri Host
        {
            get
            {
                if (PageUrl.AbsoluteUri.ToLower().StartsWith("https"))
                {
                    return new Uri("https://" + PageUrl.Host);
                }
                else
                {
                    return new Uri("http://" + PageUrl.Host);
                }
            }
        }
        public Uri PageUrl { get; set; }
        public Methods Method { get; set; }
        public bool Scccess { get; private set; }
        public WebProxy Proxy { get; set; }
        public bool Success { get; private set; }
        public int TimeOut { get; set; }
        public string Referer { get; set; }
        private void ReSetCookie(HttpWebResponse response)
        {
            if (response.Cookies != null)
            {
                foreach (Cookie item in response.Cookies)
                {
                    if (Cookies.All(p => p.Name != item.Name))
                    {
                        Cookies.Add(item);
                    }
                    else
                    {
                        for (int i = 0; i < Cookies.Count; i++)
                        {
                            if (item.Name == Cookies[i].Name)
                            {
                                Cookies[i] = item;
                            }
                        }
                    }
                }
            }
        }

        private void InitWebRequest()
        {
            webRequest = WebRequest.Create(PageUrl) as HttpWebRequest;
            webRequest.Accept = "text/html,application/xhtml+xml,application/xml;q=0.9,image/webp,*/*;q=0.8";
            webRequest.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/33.0.1750.154 Safari/537.36";
            webRequest.AllowWriteStreamBuffering = true;
            webRequest.Referer = this.Referer;
            webRequest.KeepAlive = true;
            if (TimeOut > 0)
            {
                webRequest.Timeout = TimeOut;
                webRequest.ReadWriteTimeout = TimeOut;
            }
            if (this.Method == Methods.Get)
            {
                webRequest.Method = "GET";
            }
            else
            {
                webRequest.ContentType = "application/x-www-form-urlencoded";
                webRequest.Method = "POST";
            }
            webRequest.CookieContainer = new CookieContainer();
            string cookiesStr = string.Empty;
            foreach (var item in Cookies)
            {
                cookiesStr += string.Format("{0}={1};", item.Name, item.Value);
                item.Value = item.Value.UrlEncode();
                webRequest.CookieContainer.Add(Host, item);

            }
            if (!string.IsNullOrEmpty(cookiesStr))
            {
                webRequest.Headers["Cookie"] = cookiesStr;
            }
            foreach (var item in Header)
            {
                webRequest.Headers[item.Key] = item.Value;
            }
            if (this.Proxy != null)
            {
                webRequest.Proxy = Proxy;
            }
        }
        public void GetHtml(Action<string, WebPage> action)
        {
            InitWebRequest();

            IAsyncResult result = webRequest.BeginGetResponse(new AsyncCallback((res) =>
            {
                try
                {
                    HttpWebRequest inReq = (res.AsyncState as HttpWebRequest);
                    HttpWebResponse response = (HttpWebResponse)inReq.EndGetResponse(res);
                    System.IO.Stream stream = response.GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(stream);
                    string re = reader.ReadToEnd();
                    ReSetCookie(response);
                    this.Success = true;
                    action(re, this);
                }
                catch (Exception ex)
                {
                    this.Success = false;
                    action(ex.Message, this);
                }
            }), webRequest);
        }
        public string GetHtml()
        {
            InitWebRequest();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            System.IO.Stream stream = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(stream);
            string re = reader.ReadToEnd();
            ReSetCookie(response);
            return re;
        }
        public void GetResponseStream(Action<System.IO.Stream, WebPage> action)
        {
            InitWebRequest();
            IAsyncResult result = webRequest.BeginGetResponse(new AsyncCallback((iResult) =>
            {
                HttpWebRequest inReq = (iResult.AsyncState as HttpWebRequest);
                HttpWebResponse response = (HttpWebResponse)inReq.EndGetResponse(iResult);
                System.IO.Stream streamRe = response.GetResponseStream();
                ReSetCookie(response);
                action(streamRe, this);

            }), webRequest);
        }
        public System.IO.Stream GetResponseStream()
        {
            InitWebRequest();
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            System.IO.Stream streamRe = response.GetResponseStream();
            ReSetCookie(response);
            return streamRe;
        }
        public void PostData(string data, Action<string, WebPage> action)
        {
            this.Method = Methods.POST;
            InitWebRequest();
            webRequest.BeginGetRequestStream(new AsyncCallback((asyncResult) =>
            {
                HttpWebRequest inReq = (asyncResult.AsyncState as HttpWebRequest);
                using (System.IO.Stream stream = inReq.EndGetRequestStream(asyncResult))
                {
                    byte[] buffer = Encoding.UTF8.GetBytes(data);
                    stream.Write(buffer, 0, buffer.Length);
                }
                inReq.BeginGetResponse(new AsyncCallback((asyncResultPo) =>
                {
                    HttpWebRequest inReqIn = (asyncResultPo.AsyncState as HttpWebRequest);
                    HttpWebResponse response = (HttpWebResponse)inReqIn.EndGetResponse(asyncResultPo);
                    System.IO.Stream streamResponse = response.GetResponseStream();
                    System.IO.StreamReader reader = new System.IO.StreamReader(streamResponse);
                    string re = reader.ReadToEnd();
                    ReSetCookie(response);
                    action(re, this);

                }), inReq);
            }), webRequest);
        }

        public string PostData(string data)
        {
            this.Method = Methods.POST;
            InitWebRequest();
            using (System.IO.Stream stream = webRequest.GetRequestStream())
            {
                byte[] buffer = Encoding.UTF8.GetBytes(data);
                stream.Write(buffer, 0, buffer.Length);
            }
            HttpWebResponse response = (HttpWebResponse)webRequest.GetResponse();
            System.IO.Stream streamResponse = response.GetResponseStream();
            System.IO.StreamReader reader = new System.IO.StreamReader(streamResponse);
            string re = reader.ReadToEnd();
            ReSetCookie(response);
            return re;
        }

        public string UploadFile(string formItemName, string filePath, Dictionary<string, string> othData)
        {
            byte[] beginBuff = Encoding.UTF8.GetBytes("------WebKitFormBoundaryQiOnR7KX03DvV4iK\r\n");
            byte[] endBuff = Encoding.UTF8.GetBytes("\r\n------WebKitFormBoundaryQiOnR7KX03DvV4iK--\r\n");
            byte[] centerEndBuff = Encoding.UTF8.GetBytes("\r\n------WebKitFormBoundaryQiOnR7KX03DvV4iK\r\n");
            this.Method = Methods.POST;
            InitWebRequest();
            this.webRequest.ContentType = "multipart/form-data; boundary=----WebKitFormBoundaryQiOnR7KX03DvV4iK";
            //this.webRequest.ContentLength = fs.Length + startBuff.Length + endBuff.Length;
            System.IO.Stream stream = webRequest.GetRequestStream();
            stream.Write(beginBuff, 0, beginBuff.Length);//Start

            foreach (var item in othData)
            {
                byte[] textStartBuff = Encoding.UTF8.GetBytes(string.Format("Content-Disposition: form-data; name=\"{0}\"\r\n\r\n", item.Key));
                stream.Write(textStartBuff, 0, textStartBuff.Length);
                byte[] text = Encoding.UTF8.GetBytes(item.Value);
                stream.Write(text, 0, text.Length);
                stream.Write(centerEndBuff, 0, centerEndBuff.Length);
            }
            byte[] fileStartBuff = Encoding.UTF8.GetBytes("Content-Disposition: form-data; name=\"" + formItemName + "\"; filename=\"" + System.IO.Path.GetFileName(filePath) + "\"\r\nContent-Type: application/octet-stream\r\n\r\n");

            stream.Write(fileStartBuff, 0, fileStartBuff.Length);
            byte[] readBuff = new byte[1024];
            int len = 0;
            System.IO.FileStream fs = new System.IO.FileStream(filePath, System.IO.FileMode.Open);
            while ((len = fs.Read(readBuff, 0, readBuff.Length)) > 0)
            {
                stream.Write(readBuff, 0, len);
            }
            fs.Close();
            fs.Dispose();
            stream.Write(endBuff, 0, endBuff.Length);
            System.IO.StreamReader reader = new System.IO.StreamReader(webRequest.GetResponse().GetResponseStream());
            return reader.ReadToEnd();
        }
    }
}
