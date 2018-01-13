using Passenger.Infrastructure.Extensions;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System;

namespace Passenger.Infrastructure.Services
{
    public class Encrypter : IEncrypter
    {
        private static readonly int DeriveBytesIterationsCount = 10000;
        private static readonly int SaltSize = 40;

        public string GetSalt()
        {
            var random = new Random();
            var saltBytes = new byte[SaltSize];
            var rng = RandomNumberGenerator.Create();

            return Convert.ToBase64String(saltBytes);
        }
        
        public string GetHash(string value, string salt)
        {
            if(value.Empty())
            {
                throw new ArgumentException("Can not generate hash from an empty value.", nameof(value));
            }
            if(salt.Empty())
            {
                throw new ArgumentException("Can not generate salt from an empty value.", nameof(salt));
            }

            var pbkdf2 = new Rfc2898DeriveBytes(value, GetBytes(salt), DeriveBytesIterationsCount);

            return Convert.ToBase64String(pbkdf2.GetBytes(SaltSize));
        }

        private static byte[] GetBytes(string value)
        {
            var bytes = new byte[value.Length*sizeof(char)];
            Buffer.BlockCopy(value.ToCharArray(), 0, bytes, 0, bytes.Length);

            return bytes;
        }


    }
}