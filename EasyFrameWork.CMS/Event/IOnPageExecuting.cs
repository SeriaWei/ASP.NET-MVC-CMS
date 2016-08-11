using System.Web;
using Easy.IOC;

namespace Easy.Web.CMS.Event
{
    public interface IOnPageExecuting : IDependency
    {
        void OnExecuting(HttpContext context);
    }
}