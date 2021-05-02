using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Movie_Store_API.Extensions
{
    public class PasswordHelper
    {
        public static string GetEncrypt(string password)
        {
            return CreateMD5(CreateSHA256(password));
        }

        private static string CreateMD5(string password)
        {
            // Use input string to calculate MD5 hash
            using (MD5 md5 = MD5.Create())
            {
                byte[] inputBytes = Encoding.ASCII.GetBytes(password);
                byte[] hashBytes = md5.ComputeHash(inputBytes);

                // Convert the byte array to hexadecimal string
                StringBuilder sb = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    sb.Append(hashBytes[i].ToString("X2"));
                }
                return sb.ToString();
            }
        }

        private static string CreateSHA256(string password)
        {
            string hashing = string.Empty;
            using (SHA256 hashes = SHA256.Create())
            {
                byte[] data = hashes.ComputeHash(Encoding.UTF8.GetBytes(password));
                foreach (byte b in data)
                {
                    hashing += b.ToString("x2");
                }
            }
            return hashing;
        }
    }
}
