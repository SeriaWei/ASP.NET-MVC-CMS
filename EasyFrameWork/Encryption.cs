using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;
using System.IO;
using System.Text;

namespace Easy
{
    /// <summary>
    /// 加密算法
    /// </summary>
    public class EncryptionTool
    {
        private const string initVector = "WayneEasyFrameWork";

        private const string encrypassword = "SeriaWei";

        private const int keysize = 256;

        public static string Encryption(object target)
        {
            string orginaltarget = target.ToString();
            string targetType = target.GetType().ToString();
            byte[] initVectorBytes = Encoding.UTF8.GetBytes(initVector);
            byte[] plainTextBytes = Encoding.UTF8.GetBytes(orginaltarget);
            PasswordDeriveBytes password = new PasswordDeriveBytes(encrypassword, null);
            byte[] keyBytes = password.GetBytes(keysize / 8);
            RijndaelManaged symmetricKey = new RijndaelManaged();
            symmetricKey.Mode = CipherMode.CBC;
            ICryptoTransform encryptor = symmetricKey.CreateEncryptor(keyBytes, initVectorBytes);
            MemoryStream memoryStream = new MemoryStream();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, encryptor, CryptoStreamMode.Write);
            cryptoStream.Write(plainTextBytes, 0, plainTextBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherTextBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();
            return Convert.ToBase64String(cipherTextBytes);
        }

        public static object Decryption(string target)
        {
            try
            {
                if (string.IsNullOrEmpty(target)) return target;
                byte[] initVectorBytes = Encoding.ASCII.GetBytes(initVector);
                byte[] cipherTextBytes = Convert.FromBase64String(target);
                PasswordDeriveBytes password = new PasswordDeriveBytes(encrypassword, null);
                byte[] keyBytes = password.GetBytes(keysize / 8);
                RijndaelManaged symmetricKey = new RijndaelManaged();
                symmetricKey.Mode = CipherMode.CBC;
                ICryptoTransform decryptor = symmetricKey.CreateDecryptor(keyBytes, initVectorBytes);
                MemoryStream memoryStream = new MemoryStream(cipherTextBytes);
                CryptoStream cryptoStream = new CryptoStream(memoryStream, decryptor, CryptoStreamMode.Read);
                byte[] plainTextBytes = new byte[cipherTextBytes.Length];
                int decryptedByteCount = cryptoStream.Read(plainTextBytes, 0, plainTextBytes.Length);
                memoryStream.Close();
                cryptoStream.Close();
                return Encoding.UTF8.GetString(plainTextBytes, 0, decryptedByteCount);
            }
            catch
            {
                return target;
            }
        }
    }
}