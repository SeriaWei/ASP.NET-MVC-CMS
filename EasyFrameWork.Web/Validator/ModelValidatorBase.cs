/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
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
    public abstract class ModelValidatorBase<TValidatorAttribute> : ModelValidator where TValidatorAttribute : ValidationAttribute
    {

        public ModelValidatorBase(EasyModelMetaData metadata, ControllerContext context)
            : base(metadata, context)
        {

        }
        protected TValidatorAttribute Attribute { get; set; }

        protected string ErrorMessage
        {
            get
            {
                return this.Attribute.FormatErrorMessage(Metadata.GetDisplayName());
            }
        }

        public override IEnumerable<ModelValidationResult> Validate(object container)
        {
            if (!this.Attribute.IsValid(Metadata.Model))
            {
                yield return new ModelValidationResult() { Message = ErrorMessage };
            }
        }
    }
}
