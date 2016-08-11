using Easy.IOC;
using System.Web;

namespace Easy.Web.CMS.Event
{
    public interface IOnPageExecuted : IDependency
    {
        void OnExecuted(HttpContext context);
    }
}