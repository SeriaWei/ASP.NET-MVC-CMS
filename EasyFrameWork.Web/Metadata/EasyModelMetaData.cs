using Easy.Data;
using Easy.HTML.Tags;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.Metadata
{
    public class EasyModelMetaData : ModelMetadata
    {
        public EasyModelMetaData(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
            : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            if (containerType != null)
            {
                DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute(containerType);
                if (custAttribute != null)
                {
                    if (custAttribute.MetaData.HtmlTags.ContainsKey(propertyName))
                    {
                        this.HtmlTag = custAttribute.MetaData.HtmlTags[propertyName];

                        this.DisplayFormatString = this.HtmlTag.ValueFormat;
                        if (!string.IsNullOrEmpty(this.HtmlTag.DisplayName))
                        {
                            this.DisplayName = this.HtmlTag.DisplayName;
                        }
                        else
                        {
                            this.DisplayName = this.HtmlTag.Name;
                        }
                        this.EditFormatString = this.HtmlTag.ValueFormat;
                        this.IsReadOnly = this.HtmlTag.IsReadOnly;
                        this.IsRequired = this.HtmlTag.IsRequired;
                        this.Order = this.HtmlTag.OrderIndex;
                        this.ShowForDisplay = this.HtmlTag.IsShowForDisplay;
                        this.ShowForEdit = this.HtmlTag.IsShowForEdit;
                        this.TemplateHint = this.HtmlTag.TemplateName;
                    }
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(propertyName))
                    {
                        this.PropertyData = custAttribute.MetaData.PropertyDataConfig[propertyName];
                    }

                }
            }
        }

        public HtmlTagBase HtmlTag { get; set; }
        public PropertyDataInfo PropertyData { get; set; }
    }
}
