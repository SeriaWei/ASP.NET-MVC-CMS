/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Practices.ServiceLocation;

namespace Easy.ViewPort.Validator
{
    public class RemoteValidator : ValidatorBase
    {
        public RemoteValidator()
        {
            this.BaseErrorMessage = "{0}验证失败";
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
                var app = ServiceLocator.Current.GetInstance<IApplicationContext>();
                if (app != null)
                {
                    url = string.Format("{0}{1}", app.VirtualPath, url);
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
