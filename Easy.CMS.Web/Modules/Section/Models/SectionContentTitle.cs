using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Web.CMS;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentTitleMetaData))]
    public class SectionContentTitle : SectionContent
    {
        public string InnerText { get; set; }
        public string Href { get; set; }

        public override int SectionContentType
        {
            get
            {
                return (int)Types.Title;
            }
        }
    }

    class SectionContentTitleMetaData : DataViewMetaData<SectionContentTitle>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentTitle");
            DataConfig(m => m.ID).AsPrimaryKey();
            DataConfig(m => m.SectionWidgetId).Insert(true).Update(false);
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.InnerText).AsTextBox().Required();
            ViewConfig(m => m.Href).AsTextBox().AddClass("select").AddProperty("data-url", Urls.SelectPage);
        }
    }
}