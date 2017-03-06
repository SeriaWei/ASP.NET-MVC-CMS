/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.ViewPort.Validator;
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
    public class RegularModelValidator : ModelValidatorBase<RegularExpressionAttribute>
    {
        public RegularModelValidator(EasyModelMetaData metadata, ControllerContext context, RegularValidator regularvalidator)
            : base(metadata, context)
        {
            this.Attribute = new RegularExpressionAttribute(regularvalidator.Expression);
            //this.Attribute.ErrorMessageResourceType = Metadata.ContainerType;
            //this.Attribute.ErrorMessageResourceName = Metadata.PropertyName;
            this.Attribute.ErrorMessage = regularvalidator.ErrorMessage;
        }
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRegexRule(this.Attribute.ErrorMessage, this.Attribute.Pattern) };
        }
    }
}
