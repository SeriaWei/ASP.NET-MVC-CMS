using System;
using System.Collections.Generic;
using Easy.MetaData;
using Easy.Web.CMS;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentTitleMetaData)), Serializable]
    public class SectionContentTitle : SectionContent
    {
        public const string H1 = "h1";
        public const string H2 = "h2";
        public const string H3 = "h3";
        public const string H4 = "h4";
        public const string H5 = "h5";
        public const string H6 = "h6";
        public string InnerText { get; set; }
        public string Href { get; set; }
        public string TitleLevel { get; set; }
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
            ViewConfig(m => m.TitleLevel).AsDropDownList().DataSource(() => new Dictionary<string, string>
            {
                {SectionContentTitle.H1,"一级标题"},
                {SectionContentTitle.H2,"二级标题"},
                {SectionContentTitle.H3,"三级标题"},
                {SectionContentTitle.H4,"四级标题"},
                {SectionContentTitle.H5,"五级标题"},
                {SectionContentTitle.H6,"六级标题"}
            });
        }
    }
}