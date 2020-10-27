using Microsoft.Practices.EnterpriseLibrary.Common.Configuration;
using Microsoft.Practices.EnterpriseLibrary.Security.Cryptography;

namespace TechnocomShared.Encryption
{
    public static class EncryptionHelper
    {
        private static readonly CryptographyManager DefaultCrypto =
            EnterpriseLibraryContainer.Current.GetInstance<CryptographyManager>();

        /// <summary>
        /// Gets the hashed string.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <returns></returns>
        public static string GetHashedString(string source)
        {
            return DefaultCrypto.CreateHash("TechnocomHasher", source);
        }

        /// <summary>
        /// Compares the hash.
        /// </summary>
        /// <param name="sourcePassword">The source password.</param>
        /// <param name="hashedPassword">The hashed password.</param>
        /// <returns></returns>
        public static bool CompareHash(string sourcePassword, string hashedPassword)
        {
            return DefaultCrypto.CompareHash("TechnocomHasher", sourcePassword, hashedPassword);
        }
    }
}