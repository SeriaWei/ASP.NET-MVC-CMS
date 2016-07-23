using System;
using System.IO;
using System.Security.Cryptography;
using Microsoft.Practices.ServiceLocation;

namespace Easy.Web.CMS.Encrypt
{
    public class EncryptService : IEncryptService
    {
        private const string Path = "~/Encrypt/";
        private const string PrivateKeyFile = "Private.config";
        private const string PublicKeyFile = "Public.config";
        public byte[] Encrypt(byte[] source)
        {
            Func<byte[], byte[]> encrypt = sou =>
            {
                CMSApplicationContext applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>() as CMSApplicationContext;
                using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                {
                    string folder = applicationContext.MapPath(Path);
                    if (!Directory.Exists(folder))
                    {
                        Directory.CreateDirectory(folder);
                    }
                    if (!File.Exists(folder + PrivateKeyFile))
                    {
                        File.WriteAllText(folder + PrivateKeyFile, rsa.ToXmlString(true));
                        File.WriteAllText(folder + PublicKeyFile, rsa.ToXmlString(false));
                    }
                    rsa.FromXmlString(File.ReadAllText(folder + PublicKeyFile));
                    int maxBlockSize = rsa.KeySize / 8 - 11;
                    if (sou.Length <= maxBlockSize)
                        return rsa.Encrypt(sou, false);

                    using (MemoryStream plaiStream = new MemoryStream(sou))
                    {
                        using (MemoryStream crypStream = new MemoryStream())
                        {
                            Byte[] buffer = new Byte[maxBlockSize];
                            int blockSize = plaiStream.Read(buffer, 0, maxBlockSize);

                            while (blockSize > 0)
                            {
                                Byte[] toEncrypt = new Byte[blockSize];
                                Array.Copy(buffer, 0, toEncrypt, 0, blockSize);

                                Byte[] cryptograph = rsa.Encrypt(toEncrypt, false);
                                crypStream.Write(cryptograph, 0, cryptograph.Length);

                                blockSize = plaiStream.Read(buffer, 0, maxBlockSize);
                            }

                            return crypStream.ToArray();
                        }
                    }
                }
            };
            return MarkData(encrypt(source));
        }

        public byte[] Decrypt(byte[] source)
        {
            if (IsEncrypt(source))
            {
                Func<byte[], byte[]> decrypt = sou =>
                {
                    CMSApplicationContext applicationContext = ServiceLocator.Current.GetInstance<IApplicationContext>() as CMSApplicationContext;
                    using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
                    {
                        string folder = applicationContext.MapPath(Path);
                        if (File.Exists(folder + PrivateKeyFile))
                        {
                            rsa.FromXmlString(File.ReadAllText(folder + PrivateKeyFile));
                        }
                        int maxBlockSize = rsa.KeySize / 8;

                        if (sou.Length <= maxBlockSize)
                            return rsa.Decrypt(sou, false);

                        using (MemoryStream crypStream = new MemoryStream(sou))
                        {
                            using (MemoryStream plaiStream = new MemoryStream())
                            {
                                Byte[] buffer = new Byte[maxBlockSize];
                                int blockSize = crypStream.Read(buffer, 0, maxBlockSize);

                                while (blockSize > 0)
                                {
                                    Byte[] toDecrypt = new Byte[blockSize];
                                    Array.Copy(buffer, 0, toDecrypt, 0, blockSize);

                                    Byte[] plaintext = rsa.Decrypt(toDecrypt, false);
                                    plaiStream.Write(plaintext, 0, plaintext.Length);

                                    blockSize = crypStream.Read(buffer, 0, maxBlockSize);
                                }

                                return plaiStream.ToArray();
                            }
                        }
                    }
                };
                return decrypt(ClearDataMark(source));
            }
            return source;
        }
        private byte[] MarkData(byte[] source)
        {
            byte[] newBytes = new byte[source.Length + 200];
            for (int i = 0; i < newBytes.Length; i++)
            {
                if (i < 100 || i > newBytes.Length - 100 - 1)
                {
                    newBytes[i] = 0;
                }
                else
                {
                    newBytes[i] = source[i - 100];
                }
            }
            return newBytes;
        }

        private byte[] ClearDataMark(byte[] source)
        {
            byte[] newBytes = new byte[source.Length - 200];
            for (int i = 100; i < source.Length - 100; i++)
            {
                newBytes[i - 100] = source[i];
            }
            return newBytes;
        }
        private bool IsEncrypt(byte[] source)
        {
            for (int i = 0; i < 100; i++)
            {
                if (source[i] != 0 || source[source.Length - i - 1] != 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}