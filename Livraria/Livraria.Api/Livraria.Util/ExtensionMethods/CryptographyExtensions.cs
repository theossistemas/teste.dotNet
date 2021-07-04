using System;
using System.Security.Cryptography;
using System.Text;

namespace Livraria.Util.ExtensionMethods
{
    public static class CryptographyExtensions
    {
        public static string HashString(this string inputString)
        {
            SHA512 shaM = new SHA512Managed();
            var inputByteArray = Encoding.UTF8.GetBytes(inputString);
            byte[] arrbyte = shaM.ComputeHash(inputByteArray);
            return Convert.ToBase64String(arrbyte);
        }
    }
}
