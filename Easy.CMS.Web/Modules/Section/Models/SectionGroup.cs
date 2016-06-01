using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Easy.CMS.Section.Service;
using Easy.Extend;
using Easy.MetaData;
using Easy.Models;
using Microsoft.Practices.ServiceLocation;

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
        public string PercentWidth { get; set; }
        public bool IsLoadDefaultData { get; set; }
        public IEnumerable<SectionContent> SectionContents { get; set; }

        private T GetContent<T>(SectionContent.Types type) where T : SectionContent
        {
            if (SectionContents != null)
            {
                return (T)SectionContents.FirstOrDefault(m => m != null && m.SectionContentType == (int)type);
            }
            return null;
        }

        public SectionContentTitle SectionTitle
        {
            get
            {
                return GetContent<SectionContentTitle>(SectionContent.Types.Title);
            }
        }

        public SectionContentCallToAction CallToAction
        {
            get
            {
                return GetContent<SectionContentCallToAction>(SectionContent.Types.CallToAction);
            }
        }

        public SectionContentImage SectionImage
        {
            get
            {
                return GetContent<SectionContentImage>(SectionContent.Types.Image);
            }
        }
        public SectionContentParagraph Paragraph
        {
            get
            {
                return GetContent<SectionContentParagraph>(SectionContent.Types.Paragraph);
            }
        }
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
            DataConfig(m => m.IsLoadDefaultData).Ignore();
        }

        protected override void ViewConfigure()
        {
            ViewConfig(m => m.GroupName).AsTextBox().Required();
            ViewConfig(m => m.ID).AsHidden();
            ViewConfig(m => m.SectionWidgetId).AsHidden();
            ViewConfig(m => m.IsLoadDefaultData).AsHidden().Ignore();
            ViewConfig(m => m.PartialView).AsDropDownList().DataSource(() =>
            {
                return ServiceLocator.Current.GetInstance<ISectionTemplateService>()
                    .Get()
                    .ToDictionary(m => m.TemplateName, m => m.Title);
            });
        }
    }
}