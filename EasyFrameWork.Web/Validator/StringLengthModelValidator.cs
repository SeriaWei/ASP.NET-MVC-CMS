using Easy.HTML.Validator;
using Easy.Web.Metadata;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.Validator
{
    /// <summary>
    /// 正则验证
    /// </summary>
    public class StringLengthModelValidator : ModelValidatorBase<StringLengthAttribute>
    {
        int minLength = 0;
        public StringLengthModelValidator(EasyModelMetaData metadata, ControllerContext context, StringLengthValidator stringlengthvalidator)
            : base(metadata, context)
        {
            minLength = stringlengthvalidator.Min;
            this.Attribute = new StringLengthAttribute(stringlengthvalidator.Max);
            //this.Attribute.ErrorMessageResourceType = Metadata.ContainerType;
            //this.Attribute.ErrorMessageResourceName = Metadata.PropertyName;
            this.Attribute.ErrorMessage = stringlengthvalidator.ErrorMessage;
        }
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationStringLengthRule(this.Attribute.ErrorMessage, minLength, this.Attribute.MaximumLength) };
        }
    }
}
