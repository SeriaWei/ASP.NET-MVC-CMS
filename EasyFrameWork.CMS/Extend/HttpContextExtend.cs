/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System.Web;
using Easy.Web.CMS.Layout;

namespace Easy.Web.CMS
{
    public static class HttpContextExtend
    {
        public static void TrySetLayout(this HttpContextBase httpContext, LayoutEntity layout)
        {
            if (!httpContext.Items.Contains(layout))
            {
                httpContext.Items.Add(StringKeys.LayoutItem, layout);
            }
        }
        public static LayoutEntity GetLayout(this HttpContextBase httpContext)
        {
            return httpContext.Items[StringKeys.LayoutItem] as LayoutEntity;
        }
    }
}
