using Easy.CMS.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;

namespace Easy.CMS.Common.Models
{
    [DataConfigure(typeof(NavigationWidgetMetaData))]
    public class NavigationWidget : WidgetBase
    {
    }
    class NavigationWidgetMetaData : DataViewMetaData<NavigationWidget>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }
        protected override void DataConfigure()
        {
            DataTable("NavigationWidget");
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.WidgetName).AsTextBox().Order(1).Required();
            ViewConfig(m => m.ZoneId).AsDropDownList().Order(2).DataSource(ViewDataKeys.Zones, Easy.Constant.SourceType.ViewData).Required();
            ViewConfig(m => m.Position).AsTextBox().Order(3).RegularExpression(Constant.RegularExpression.Integer);
        }
    }

}