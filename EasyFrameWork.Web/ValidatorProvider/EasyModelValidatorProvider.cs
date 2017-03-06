/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.Web.Metadata;
using Easy.ViewPort.Validator;
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
            if (data != null && data.ViewPortDescriptor != null)
            {
                validators.AddRange(data.ViewPortDescriptor.Validator.Select(item => GetValidator(data, context, item)).Where(validator => validator != null));
            }
            return validators;
        }
        private ModelValidator GetValidator(EasyModelMetaData metadata, ControllerContext context, ValidatorBase validator)
        {
            if (string.IsNullOrEmpty(validator.DisplayName))
            {
                validator.DisplayName = string.IsNullOrEmpty(metadata.ViewPortDescriptor.DisplayName) ? metadata.ViewPortDescriptor.Name : metadata.ViewPortDescriptor.DisplayName;
            }
            if (validator is RequiredValidator)
            {
                return new Validator.RequiredModelValidator(metadata, context, (RequiredValidator)validator);
            }
            if (validator is RangeValidator)
            {
                return new Validator.RangeModelValidator(metadata, context, (RangeValidator)validator);
            }
            if (validator is RegularValidator)
            {
                return new Validator.RegularModelValidator(metadata, context, (RegularValidator)validator);
            }
            if (validator is RemoteValidator)
            {
                return new Validator.RemoteModelValidator(metadata, context, (RemoteValidator)validator);
            }
            if (validator is StringLengthValidator)
            {
                return new Validator.StringLengthModelValidator(metadata, context, (StringLengthValidator)validator);
            }
            return null;
        }
    }
}
