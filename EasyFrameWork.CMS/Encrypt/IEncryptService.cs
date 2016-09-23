/* http://www.zkea.net/ Copyright 2016 ZKEASOFT http://www.zkea.net/licenses */
using Easy.IOC;

namespace Easy.Web.CMS.Encrypt
{
    public interface IEncryptService : IDependency
    {
        byte[] Encrypt(byte[] source);
        byte[] Decrypt(byte[] source);
    }
}