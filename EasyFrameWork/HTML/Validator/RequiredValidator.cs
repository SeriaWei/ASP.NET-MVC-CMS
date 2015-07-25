using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Easy.Extend;

namespace Easy.HTML.Validator
{
    public class RequiredValidator : ValidatorBase
    {
        public RequiredValidator()
        {
            this.BaseErrorMessage = "{0} 不可空";
        }
        public override bool Validate(object value)
        {
            if (value == null || value.ToString().IsNullOrEmpty()) return false;
            else return true;
        }
    }
}
