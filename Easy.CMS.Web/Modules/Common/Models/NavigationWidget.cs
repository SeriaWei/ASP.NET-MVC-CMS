using Easy.Web.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS.MetaData;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(NavigationWidgetMetaData))]
    public class NavigationWidget : WidgetBase
    {
        public string Title { get; set; }
        public string Logo { get; set; }
        public string CustomerClass { get; set; }
    }
    class NavigationWidgetMetaData : WidgetMetaData<NavigationWidget>
    {
        protected override void ViewConfigure()
        {
            base.ViewConfigure();
            ViewConfig(m => m.Title).AsTextBox();
            ViewConfig(m => m.CustomerClass).AsHidden();
        }
    }

}