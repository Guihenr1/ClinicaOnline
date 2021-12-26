using System;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace ClinicaOnline.Core.Utils
{
    public static class Security
    {
        public static string GenerateHash(string password, byte[] salt)
        {
            return Convert.ToBase64String(KeyDerivation.Pbkdf2(
                password: password,
                salt: salt,
                prf: KeyDerivationPrf.HMACSHA256,
                iterationCount: 100000,
                numBytesRequested: 256 / 8));
        }
    }
}