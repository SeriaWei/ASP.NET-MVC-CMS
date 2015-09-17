using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;

namespace Easy.CMS.Section.Models
{
    [DataConfigure(typeof(SectionGroupMetaData))]
    public class SectionGroup : EditorEntity
    {
        public int? ID { get; set; }
        public string GroupName { get; set; }
        public string SectionWidgetId { get; set; }
        public string PartialView { get; set; }
        public int? Order { get; set; }

        public IEnumerable<SectionContent> SectionContents { get; set; }
    }

    class SectionGroupMetaData : DataViewMetaData<SectionGroup>
    {
        protected override bool IsIgnoreBase()
        {
            return true;
        }

        protected override void DataConfigure()
        {
            DataTable("SectionGroup");
            DataConfig(m => m.ID).AsIncreasePrimaryKey();
            DataConfig(m => m.Title).Ignore();
            DataConfig(m => m.Description).Ignore();
            DataConfig(m => m.Status).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.GroupName).AsTextBox().Required();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.SectionWidgetId).AsHidden();
            ViewConfig(m => m.PartialView).AsDropDownList().DataSource(() =>
            {
                string folder = AppDomain.CurrentDomain.BaseDirectory + "Modules/Section/Views";
                Dictionary<string, string> result = new Dictionary<string, string>
                {
                    {"SectionTemplate.Default", "Default"}
                };
                Directory.GetFiles(folder, "SectionTemplate.*.cshtml").Each(f =>
                {
                    string fileName = Path.GetFileNameWithoutExtension(f);
                    if (fileName.IsNotNullAndWhiteSpace() && !result.ContainsKey(fileName))
                    {
                        result.Add(fileName, fileName.Replace("SectionTemplate.", ""));
                    }
                });
                return result;
            });
        }
    }
}