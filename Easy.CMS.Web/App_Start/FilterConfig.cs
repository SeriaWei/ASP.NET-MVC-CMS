using System.Web.Mvc;
using Easy.Web.Attribute;

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