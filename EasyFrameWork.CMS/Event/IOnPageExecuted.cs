/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.IOC;
using Easy.Web.CMS.Page;

namespace Easy.Web.CMS.Event
{
    public interface IOnPageExecuted : IDependency
    {
        void OnExecuted(PageEntity currentPage, HttpContext context);
    }
}