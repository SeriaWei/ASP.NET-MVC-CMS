using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Easy.HTML.Validator
{
    public class RegularValidator : ValidatorBase
    {
        public RegularValidator(string expression)
        {
            this.Expression = expression;
            this.BaseErrorMessage = "{0}的输入的值不舒合要求";
        }
        public string Expression { get; set; }

        public override bool Validate(object value)
        {
            if (value == null) return true;
           return Regex.IsMatch(value.ToString(), this.Expression);
        }
    }
}
