using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Validator
{
    public abstract class ValidatorBase
    {
        public string Property { get; set; }
        public string DisplayName { get; set; }
        public string BaseErrorMessage { get; protected set; }
        string _ErrorMessage;
        public string ErrorMessage
        {
            get
            {
                if (string.IsNullOrEmpty(_ErrorMessage))
                {
                    return string.Format(BaseErrorMessage, DisplayName ?? Property);
                }
                else
                {
                    return _ErrorMessage;
                }
            }
            set { _ErrorMessage = value; }
        }
        public abstract bool Validate(object value);
    }
}
