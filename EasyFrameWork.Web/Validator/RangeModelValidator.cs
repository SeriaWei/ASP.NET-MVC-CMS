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
    /// 范围验证
    /// </summary>
    public class RangeModelValidator : ModelValidatorBase<RangeAttribute>
    {
        public RangeModelValidator(EasyModelMetaData metadata, ControllerContext context, RangeValidator rangevalidator)
            : base(metadata, context)
        {
            this.Attribute = new RangeAttribute(rangevalidator.Min, rangevalidator.Max);
            //this.Attribute.ErrorMessageResourceType = Metadata.ContainerType;
            //this.Attribute.ErrorMessageResourceName = Metadata.PropertyName;
            this.Attribute.ErrorMessage = rangevalidator.ErrorMessage;
        }
        public override IEnumerable<ModelClientValidationRule> GetClientValidationRules()
        {
            return new[] { new ModelClientValidationRangeRule(this.Attribute.ErrorMessage, this.Attribute.Minimum, this.Attribute.Maximum) };
        }
    }
}
