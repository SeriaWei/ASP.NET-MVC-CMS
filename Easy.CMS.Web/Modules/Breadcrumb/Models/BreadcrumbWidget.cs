using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;
using Easy.Web.CMS.Widget;

namespace Easy.CMS.Breadcrumb.Models
{
    [DataConfigure(typeof(BreadcrumbWidgetMetaData)), Serializable]
    public class BreadcrumbWidget : WidgetBase
    {
        public bool IsLinkAble { get; set; }
    }

    class BreadcrumbWidgetMetaData : WidgetMetaData<BreadcrumbWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.IsLinkAble).AsHidden();
        }
    }
}