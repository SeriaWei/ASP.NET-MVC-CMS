using System.Web;
using System.Web.Mvc;
using Easy.Web.Attribute;
using Easy.Web.CMS.Filter;

namespace Easy.CMS.Web
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorToLogAttribute());
            //filters.Add(new AuthorizeAttribute());
        }
    }
}