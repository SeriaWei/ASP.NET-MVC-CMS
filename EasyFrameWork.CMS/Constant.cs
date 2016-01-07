using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS
{
    public class ViewDataKeys
    {
        public const string Zones = "ViewDataKey_Zones";
        public const string Layouts = "ViewDataKey_Layouts";
        public const string ArticleCategory = "ViewDataKey_ArticleType";
        public const string ProductCategory = "ViewDataKey_ProductCategory";
    }
    public class ReView
    {
        public const string QueryKey = "ViewType";
        public const string Review = "Review";
    }

    public class CacheTrigger
    {
        public const string WidgetChanged = "WidgetChanged";
    }

    public class Urls
    {
        public const string SelectPage = "/admin/page/select";
        public const string SelectImage = "/Scripts/ImagePlug/index.html";
    }
}
