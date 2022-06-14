using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;

namespace WebApplication1.Help
{
    public static class Security
    {

        public static Tuple<string, string> HashedPasswordSalt(string passwd)
        {
            byte[] salt = new byte[16];
            using var gen = RandomNumberGenerator.Create();
            gen.GetBytes(salt);

            string hashpasswd = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: passwd, salt: salt, prf: KeyDerivationPrf.HMACSHA1, iterationCount: 10000, numBytesRequested: 32));
            string x = Convert.ToBase64String(salt);

            return new(hashpasswd, x);
        }

        public static string HashedSaltedPasswd(string passwd, string salt)
        {
            byte[] saltbyte = Convert.FromBase64String(salt);
            var hashedSaltedPasswd = Convert.ToBase64String(KeyDerivation.Pbkdf2(password: passwd, salt: saltbyte, prf: KeyDerivationPrf.HMACSHA1, iterationCount: 10000, numBytesRequested: 32));
            return hashedSaltedPasswd;
        }




    }

}