/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;

namespace Easy.Web.CMS.Validate
{
    public interface IValidateService : IDependency
    {
        byte[] GenerateCode();
        bool ValidateCode(string code);
    }
}
