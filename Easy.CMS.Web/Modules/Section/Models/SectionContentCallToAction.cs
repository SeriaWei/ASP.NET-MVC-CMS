using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionContentCallToActionMetaData))]
    public class SectionContentCallToAction : SectionContent
    {
        public string InnerText { get; set; }
        public string Href { get; set; }
    }

    class SectionContentCallToActionMetaData : DataViewMetaData<SectionContentCallToAction>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionContentCallToAction");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {

        }
    }
}