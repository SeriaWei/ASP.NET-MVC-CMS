/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Net;
using System.Web.Mvc;
namespace Easy.Web.HttpActionResult
{
    public class HttpForbiddenResult : HttpStatusCodeResult
    {
        public HttpForbiddenResult()
            : base(HttpStatusCode.Forbidden, null)
        {
        }
    }
}