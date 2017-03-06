/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Data;
using Easy.ViewPort.Descriptor;
using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using Easy.Extend;

namespace Easy.Web.Metadata
{
    public sealed class EasyModelMetaData : ModelMetadata
    {
        public EasyModelMetaData(ModelMetadataProvider provider, Type containerType, Func<object> modelAccessor, Type modelType, string propertyName)
            : base(provider, containerType, modelAccessor, modelType, propertyName)
        {
            if (containerType != null)
            {
                DataConfigureAttribute custAttribute = DataConfigureAttribute.GetAttribute(containerType);
                if (custAttribute != null)
                {
                    custAttribute.InitDisplayName();
                    if (custAttribute.MetaData.ViewPortDescriptors.ContainsKey(propertyName))
                    {
                        ViewPortDescriptor = custAttribute.MetaData.ViewPortDescriptors[propertyName];
                        DisplayFormatString = ViewPortDescriptor.ValueFormat;

                        if (!string.IsNullOrEmpty(ViewPortDescriptor.DisplayName))
                        {
                            DisplayName = ViewPortDescriptor.DisplayName;
                        }
                        else
                        {
                            DisplayName = ViewPortDescriptor.Name;
                        }
                        EditFormatString = ViewPortDescriptor.ValueFormat;
                        IsReadOnly = ViewPortDescriptor.IsReadOnly;
                        IsRequired = ViewPortDescriptor.IsRequired;
                        Order = ViewPortDescriptor.OrderIndex;
                        ShowForDisplay = ViewPortDescriptor.IsShowForDisplay;
                        ShowForEdit = ViewPortDescriptor.IsShowForEdit;
                        TemplateHint = ViewPortDescriptor.TemplateName;
                        HideSurroundingHtml = ViewPortDescriptor.IsHidden;
                    }
                    if (custAttribute.MetaData.PropertyDataConfig.ContainsKey(propertyName))
                    {
                        PropertyData = custAttribute.MetaData.PropertyDataConfig[propertyName];
                    }
                }
            }
        }

        public BaseDescriptor ViewPortDescriptor { get; set; }
        public PropertyDataInfo PropertyData { get; set; }

        public Dictionary<string, object> GetAttributes()
        {
            if (ViewPortDescriptor == null) return null;
            Dictionary<string, object> attributes = new Dictionary<string, object>();
            attributes.Add("class", "form-control " + string.Join(" ", ViewPortDescriptor.Classes) + (this.IsRequired ? " required" : ""));

            ViewPortDescriptor.Properties.Each(m =>
            {
                attributes.Add(m.Key, m.Value);
            });
            StringBuilder style = new StringBuilder();
            ViewPortDescriptor.Styles.Each(m =>
            {
                style.AppendFormat("{0}:{1};", m.Key, m.Value);
            });
            if (style.Length > 0)
            {
                attributes.Add("style", style.ToString());
            }
            return attributes;
        }
    }
}
