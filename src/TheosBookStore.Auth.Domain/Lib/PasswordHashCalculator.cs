using System;
using System.Security.Cryptography;
using System.Text;

namespace TheosBookStore.Auth.Domain.Libs
{
    public class PasswordHashCalculator
    {
        public byte[] PasswordHash { get; private set; }
        public byte[] PasswordSalt { get; private set; }

        public PasswordHashCalculator(string password)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            CalcHash(password);
        }

        public PasswordHashCalculator(string password, byte[] key)
        {
            CalcHash(password, key);
        }

        private void CalcHash(string password, byte[] key = null)
        {
            using var hmac = new HMACSHA512(); //NOSONAR
            if (key != null)
                hmac.Key = key;
            var encodedString = Encoding.UTF8.GetBytes(password);
            PasswordSalt = hmac.Key;
            PasswordHash = hmac.ComputeHash(encodedString);
        }

    }
}
