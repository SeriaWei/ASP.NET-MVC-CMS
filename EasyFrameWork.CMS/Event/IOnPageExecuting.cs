/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.IOC;

namespace Easy.Web.CMS.Event
{
    public interface IOnPageExecuting : IDependency
    {
        void OnExecuting(HttpContext context);
    }
}