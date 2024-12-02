using AdminWebPlatform.Contracts;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

namespace AdminWebPlatform.Services
{
    public class SHA256HashService : IHasher
    {
        public string GetHash(string data)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(data, nameof(data));

            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
            const int Pbkdf2IterCount = 1000;
            const int Pbkdf2SubkeyLength = 256 / 8;
            const int SaltSize = 128 / 8;

            byte[] salt = RandomNumberGenerator.GetBytes(SaltSize);
            byte[] subkey = KeyDerivation.Pbkdf2(data, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            var outputBytes = new byte[1 + SaltSize + Pbkdf2SubkeyLength];
            outputBytes[0] = 0x00;
            Buffer.BlockCopy(salt, 0, outputBytes, 1, SaltSize);
            Buffer.BlockCopy(subkey, 0, outputBytes, 1 + SaltSize, Pbkdf2SubkeyLength);

            return Convert.ToBase64String(outputBytes);
        }

        public bool Verify(string hashedData, string data)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(hashedData, nameof(hashedData));
            ArgumentException.ThrowIfNullOrWhiteSpace(data, nameof(data));

            const KeyDerivationPrf Pbkdf2Prf = KeyDerivationPrf.HMACSHA1;
            const int Pbkdf2IterCount = 1000;
            const int Pbkdf2SubkeyLength = 256 / 8;
            const int SaltSize = 128 / 8;

            byte[] decHashedData = Convert.FromBase64String(hashedData);

            if (decHashedData.Length != 1 + SaltSize + Pbkdf2SubkeyLength)
                return false;

            byte[] salt = new byte[SaltSize];
            Buffer.BlockCopy(decHashedData, 1, salt, 0, salt.Length);

            byte[] expectedSubkey = new byte[Pbkdf2SubkeyLength];
            Buffer.BlockCopy(decHashedData, 1 + salt.Length, expectedSubkey, 0, expectedSubkey.Length);

            byte[] actualSubkey = KeyDerivation.Pbkdf2(data, salt, Pbkdf2Prf, Pbkdf2IterCount, Pbkdf2SubkeyLength);

            return CryptographicOperations.FixedTimeEquals(actualSubkey, expectedSubkey);
        }
    }
}
