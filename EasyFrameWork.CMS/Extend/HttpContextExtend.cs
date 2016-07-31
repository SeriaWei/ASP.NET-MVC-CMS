using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.CMS
{
    public static class HttpContextExtend
    {
        public static void TrySetLayout(this HttpContextBase httpContext, Layout.LayoutEntity layout)
        {
            if (!httpContext.Items.Contains(layout))
            {
                httpContext.Items.Add(StringKeys.LayoutItem, layout);
            }
        }
        public static Layout.LayoutEntity GetLayout(this HttpContextBase httpContext)
        {
            return httpContext.Items[StringKeys.LayoutItem] as Layout.LayoutEntity;
        }
    }
}
