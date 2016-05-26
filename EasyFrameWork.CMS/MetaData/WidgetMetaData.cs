using Easy.Web.CMS.Widget;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.MetaData
{
    public abstract class WidgetMetaData<T> : DataViewMetaData<T> where T : WidgetBase
    {
        int orderStart = 0;
        protected override bool IsIgnoreBase()
        {
            return true;
        }
        protected int NextOrder()
        {
            return ++orderStart;
        }
        private void InitViewBase()
        {
            ViewConfig(m => m.WidgetName).AsTextBox().Order(NextOrder()).Required();
            ViewConfig(m => m.Title).AsTextBox().Order(NextOrder());
            ViewConfig(m => m.ZoneID).AsDropDownList().Order(NextOrder()).DataSource(ViewDataKeys.Zones, Easy.Constant.SourceType.ViewData).Required();
            ViewConfig(m => m.Position).AsTextBox().Order(NextOrder()).RegularExpression(Easy.Constant.RegularExpression.Integer);
            ViewConfig(m => m.IsTemplate).AsCheckBox().Order(NextOrder());
            ViewConfig(m => m.Thumbnail).AsTextBox().Order(NextOrder()).AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia);
            ViewConfig(m => m.StyleClass).AsTextBox().Order(NextOrder()).AddClass(StringKeys.StyleEditor).AddProperty("data-url", Urls.StyleEditor).AddProperty("data-width", "1024").MaxLength(1000);
        }

        protected override void DataConfigure()
        {
            DataTable(TargetType.Name);
            DataConfig(m => m.ID).AsPrimaryKey();
        }

        protected override void ViewConfigure()
        {
            InitViewBase();
        }
    }
}
