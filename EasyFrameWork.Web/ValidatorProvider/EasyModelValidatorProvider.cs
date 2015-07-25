using Easy.Web.Metadata;
using Easy.HTML.Validator;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Easy.Web.ValidatorProvider
{
    public class EasyModelValidatorProvider : ModelValidatorProvider
    {
        public override IEnumerable<ModelValidator> GetValidators(ModelMetadata metadata, ControllerContext context)
        {
            List<ModelValidator> validators = new List<ModelValidator>();
            EasyModelMetaData data = metadata as EasyModelMetaData;
            if (data != null && data.HtmlTag != null)
            {
                foreach (ValidatorBase item in data.HtmlTag.Validator)
                {
                    ModelValidator validator = GetValidator(data, context, item);
                    if (validator != null)
                    {
                        validators.Add(validator);
                    }
                }
            }
            return validators;
        }
        private ModelValidator GetValidator(EasyModelMetaData metadata, ControllerContext context, ValidatorBase validator)
        {
            if (string.IsNullOrEmpty(validator.DisplayName))
            {
                validator.DisplayName = string.IsNullOrEmpty(metadata.HtmlTag.DisplayName) ? metadata.HtmlTag.Name : metadata.HtmlTag.DisplayName;
            }
            if (validator is RequiredValidator)
            {
                return new Validator.RequiredModelValidator(metadata, context, validator as RequiredValidator);
            }
            else if (validator is RangeValidator)
            {
                return new Validator.RangeModelValidator(metadata, context, validator as RangeValidator);
            }
            else if (validator is RegularValidator)
            {
                return new Validator.RegularModelValidator(metadata, context, validator as RegularValidator);
            }
            else if (validator is RemoteValidator)
            {
                return new Validator.RemoteModelValidator(metadata, context, validator as RemoteValidator);
            }
            else if (validator is StringLengthValidator)
            {
                return new Validator.StringLengthModelValidator(metadata, context, validator as StringLengthValidator);
            }
            else return null;
        }
    }
}
