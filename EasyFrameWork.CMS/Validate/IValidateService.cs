using Easy.IOC;

namespace Easy.Web.CMS.Validate
{
    public interface IValidateService : IDependency
    {
        byte[] GenerateCode();
        bool ValidateCode(string code);
    }
}
