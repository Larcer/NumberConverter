using System;
using System.Security.Cryptography;
using System.Text;

namespace NumberConverter.Tools
{
    // Represents a simple md5 encryptor
    public static class SimpleEncryptor
    {
        public static string EncryptText(string text)
        {
            MD5CryptoServiceProvider md5Hasher = new MD5CryptoServiceProvider();

            var hashValue = md5Hasher.ComputeHash(ConvertStringToByteArray(text));
            var hashData = BitConverter.ToString(hashValue);
            hashData = hashData.Replace("-", "");
            var result = hashData;

            return result;
        }

        private static byte[] ConvertStringToByteArray(string data)
        {
            return new UnicodeEncoding().GetBytes(data);
        }
    }
}
