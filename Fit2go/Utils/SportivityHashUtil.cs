using System.Linq;
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

        private static string GetStringFromBytes(byte[] hash) =>
            string.Concat(hash.Select(b => b.ToString("x2")));
    }
}
