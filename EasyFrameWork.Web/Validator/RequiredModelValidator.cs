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
    /// 必填验证
    /// </summary>
    public class RequiredModelValidator : ModelValidatorBase<RequiredAttribute>
    {
        public RequiredModelValidator(EasyModelMetaData metadata, ControllerContext context, RequiredValidator requiredvalidator)
            : base(metadata, context)
        {
            this.Attribute = new RequiredAttribute();
            //this.Attribute.ErrorMessageResourceType = Metadata.ContainerType;
            //this.Attribute.ErrorMessageResourceName = Metadata.PropertyName;
            this.Attribute.ErrorMessage = requiredvalidator.ErrorMessage;
        }
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRequiredRule(this.Attribute.ErrorMessage) };
        }
    }
}
