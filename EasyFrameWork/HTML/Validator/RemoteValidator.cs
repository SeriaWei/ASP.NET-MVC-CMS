using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.HTML.Validator
{
    public class RemoteValidator : ValidatorBase
    {
        public RemoteValidator()
        {
            this.BaseErrorMessage = "{0}远程验证失败";
        }
        public string Url
        {
            get
            {
                string url = string.Format("/{0}/{1}", Controller, Action);
                if (!string.IsNullOrEmpty(Area))
                {
                    url = string.Format("/{0}/{1}", Area, url);
                }
                if (!string.IsNullOrEmpty(Easy.Module.ApplicationName))
                {
                    url = string.Format("{0}{1}", Easy.Module.ApplicationName, url);
                }
                return url;
            }
        }
        public string Action { get; set; }
        public string Controller { get; set; }
        public string Area { get; set; }

        public string AdditionalFields { get; set; }

        public override bool Validate(object value)
        {
            throw new NotImplementedException();
        }
    }
}
