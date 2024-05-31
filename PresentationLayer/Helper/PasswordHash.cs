using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLayer.Helper
{
    /// <summary>
    /// Provides methods for hashing passwords.
    /// </summary>
    public static class PasswordHash
    {
        /// <summary>
        /// Computes the SHA256 hash of the specified password.
        /// </summary>
        /// <param name="password">The password to hash.</param>
        /// <returns>The hashed password as a hexadecimal string.</returns>
        public static string Hashing(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < hashedBytes.Length; i++)
                {
                    builder.Append(hashedBytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
