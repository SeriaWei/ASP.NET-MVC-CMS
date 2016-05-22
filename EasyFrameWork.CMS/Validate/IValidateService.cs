using Easy.IOC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Easy.Web.CMS.Validate
{
    public interface IValidateService : IDependency
    {
        byte[] GenerateCode();
        bool ValidateCode(string code);
    }
}
