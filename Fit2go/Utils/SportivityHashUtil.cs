using System.Security.Cryptography;
using System.Text;

namespace Fit2go.Utils
{
    public static class SportivityHashUtil
    {
        private static readonly HashAlgorithm s_hashAlgorithm = SHA1.Create();
        private static readonly Encoding s_encoding = Encoding.UTF8;

        public static string GetSha1(string value)
        {
            byte[] bytes = s_encoding.GetBytes(value);
            return GetStringFromBytes(s_hashAlgorithm.ComputeHash(bytes));
        }

        private static string GetStringFromBytes(byte[] bytes)
        {
            StringBuilder stringBuilder = new StringBuilder();
            foreach (byte b in bytes)
            {
                string text = b.ToString("x2");
                stringBuilder.Append(text);
            }
            return stringBuilder.ToString();
        }
    }
}
