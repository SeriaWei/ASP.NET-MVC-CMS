using Easy.MetaData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;
using System.Collections;

namespace Easy.HTML.Tags
{
    public class CollectionAreaTag : HtmlTagBase
    {
        Type _propertyType;
        public CollectionAreaTag(Type modelType, string property)
            : base(modelType, property)
        {
            _propertyType = this.ModelType.GetProperty(this.Name).PropertyType;
        }

        public override string ToString()
        {
            return ToString(false);
        }
        public override string ToString(bool widthLabel)
        {
            StringBuilder builder = new StringBuilder();
            if (_propertyType.IsGenericType)
            {
                var genericType = _propertyType.GetGenericArguments()[0];
                var attribute = DataConfigureAttribute.GetAttribute(genericType);
                if (widthLabel)
                {
                    builder.AppendFormat("<span class=\"input-group-addon {1}\">{0}</span>", this.DisplayName, this.IsRequired ? "required" : "");
                }
                builder.Append("<div class='input-group-collection container-fluid'>");
                builder.Append("<div class='Template' style='display:none'>");
                {
                    builder.Append("<div class='row item'>");
                    attribute.GetHtmlTags(false).Each(m =>
                    {
                        builder.Append("<div class='col-lg-3 col-md-4 col-sm-6'>");
                        builder.Append("<div class='input-group'>");
                        m.NamePreFix = this.Name + "[{0}].";
                        builder.AppendLine(m.ToString(widthLabel));
                        builder.Append("</div>");
                        builder.Append("</div>");
                    });
                    attribute.GetHtmlHiddenTags().Each(m =>
                    {
                        m.NamePreFix = this.Name + "[{0}].";
                        builder.AppendLine(m.ToString(false));
                    });
                    builder.AppendFormat("<div class='col-md-12'><input type='button' value='删除' class='btn btn-danger btn-xs delete' data-value='{0}'/></div>", Constant.ActionType.Unattached);
                    builder.Append("</div>");
                }
                builder.Append("</div>");

                builder.AppendFormat("<div class='row'><div class='col-md-12'><input type=\"button\" value=\"添加\" class='btn btn-primary btn-xs add' data-value='{0}' /></div></div>", Constant.ActionType.Create);
                if (this.Value != null)
                {
                    if (this.Value is IEnumerable)
                    {
                        int index = 0;
                        foreach (var item in (this.Value as IEnumerable))
                        {
                            builder.Append("<div class='row item'>");
                            attribute.GetHtmlTags(false).Each(m =>
                            {
                                builder.Append("<div class='col-lg-3 col-md-4 col-sm-6'>");
                                builder.Append("<div class='input-group'>");
                                m.NamePreFix = this.Name + "[{0}].".FormatWith(index);
                                m.SetValue(Easy.Reflection.ClassAction.GetObjPropertyValue(item, m.Name));
                                builder.AppendLine(m.ToString(widthLabel));
                                builder.Append("</div>");
                                builder.Append("</div>");
                            });
                            attribute.GetHtmlHiddenTags().Each(m =>
                            {
                                m.NamePreFix = this.Name + "[{0}].".FormatWith(index);
                                m.SetValue(Easy.Reflection.ClassAction.GetObjPropertyValue(item, m.Name));
                                builder.AppendLine(m.ToString(false));
                            });
                            builder.AppendFormat("<div class='col-md-12'><input type='button' value='删除' class='btn btn-danger btn-xs delete' data-value='{0}'/></div>", Constant.ActionType.Delete);
                            builder.Append("</div>");
                            index++;
                        }
                    }
                }
                builder.Append("</div>");
                return builder.ToString();
            }
            return base.ToString(widthLabel);
        }
    }
}
