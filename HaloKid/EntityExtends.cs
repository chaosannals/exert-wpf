using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Security.Cryptography;

namespace HaloKid
{
    public enum Gender : byte
    {
        Unknown = 0,
        Male = 1,
        Female = 2,
    }

    public static class EntityExtends
    {
        public static string ToSha256(this string text)
        {
            byte[] data = Encoding.UTF8.GetBytes(text);
            byte[] result = SHA256.HashData(data);
            return BitConverter.ToString(result).Replace("-", string.Empty).ToLower();
        }
    }
}
