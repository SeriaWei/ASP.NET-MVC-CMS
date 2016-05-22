using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Models;
using Easy.MetaData;

namespace Easy.Web.CMS.Theme
{
    [DataConfigure(typeof(ThemeEntityMetaData))]
    public class ThemeEntity : EditorEntity
    {
        public const string DefaultThumbnail = "~/Content/Images/theme.jpg";
        public string ID { get; set; }
        public string Url { get; set; }
        public string UrlDebugger { get; set; }
        public string Thumbnail { get; set; }
        public bool IsActived { get; set; }
        public bool IsPreView { get; set; }

    }

    class ThemeEntityMetaData : DataViewMetaData<ThemeEntity>
    {
        protected override void DataConfigure()
        {
            DataTable("CMS_Theme");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.IsPreView).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.Title).AsTextBox().Required();
            ViewConfig(m => m.Url).AsTextBox().Required().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia); ;
            ViewConfig(m => m.Thumbnail).AsTextBox().Required().AddClass(StringKeys.SelectImageClass).AddProperty("data-url", Urls.SelectMedia); ;
            ViewConfig(m => m.Description).AsTextArea();
        }
    }
}
