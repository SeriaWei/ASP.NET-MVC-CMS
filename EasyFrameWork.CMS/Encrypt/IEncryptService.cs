using Easy.IOC;

namespace Easy.Web.CMS.Encrypt
{
    public interface IEncryptService : IDependency
    {
        byte[] Encrypt(byte[] source);
        byte[] Decrypt(byte[] source);
    }
}