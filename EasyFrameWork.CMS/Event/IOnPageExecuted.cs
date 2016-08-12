using Easy.IOC;
using System.Web;
using Easy.Web.CMS.Page;

namespace Easy.Web.CMS.Event
{
    public interface IOnPageExecuted : IDependency
    {
        void OnExecuted(PageEntity currentPage, HttpContext context);
    }
}