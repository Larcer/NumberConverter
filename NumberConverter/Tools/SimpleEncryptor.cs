using System;
using System.Security.Cryptography;
using System.Text;

namespace Nameless.NumberConverter.Tools
{
    // Represents a simple md5 encryptor
    internal static class SimpleEncryptor
    {
        internal static string EncryptText(string text)
        {
            var md5Hasher = new MD5CryptoServiceProvider();
            var hashValue = md5Hasher.ComputeHash(ConvertStringToByteArray(text));
            var hashData = BitConverter.ToString(hashValue);

            return hashData.Replace("-", "");
        }

        private static byte[] ConvertStringToByteArray(string data)
        {
            return new UnicodeEncoding().GetBytes(data);
        }
    }
}
