namespace SiCo.Utilities.Crypto
{
    using System.Security.Cryptography;

    /// <summary>
    /// SHA Helper
    /// </summary>
    public static class SHA
    {
        /// <summary>
        /// Get SHA1 hash for given string
        /// </summary>
        /// <param name="hashMe">this/input string</param>
        /// <returns>Hash Byte</returns>
        public static byte[] HashSHA1(string hashMe)
        {
            byte[] result;

#if NETSTANDARD1_6
            using (SHA1 sha = SHA1.Create())
            {
                result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashMe));
            }
#else
            using (SHA1 shaM = new SHA1Managed())
            {
                result = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashMe));
            }
#endif

            return result;
        }


		/// <summary>
		/// Get SHA1 hash for given string
		/// </summary>
		/// <param name="hashMe">this/input bytes</param>
		/// <returns>Hash Byte</returns>
		public static byte[] HashSHA1(byte[] hashMe)
		{
			byte[] result;

#if NETSTANDARD1_6
			using (SHA1 sha = SHA1.Create())
			{
				result = sha.ComputeHash(hashMe);
			}
#else
            using (SHA1 shaM = new SHA1Managed())
            {
                result = shaM.ComputeHash(hashMe);
            }
#endif

			return result;
		}

		/// <summary>
		/// Get SHA512 hash for given string
		/// </summary>
		/// <param name="hashMe">this/input string</param>
		/// <returns>Hash Byte</returns>
		public static byte[] HashSHA512(string hashMe)
        {
            byte[] result;

#if NETSTANDARD1_6
            using (SHA512 sha = SHA512.Create())
            {
                result = sha.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashMe));
            }
#else
            using (SHA512 shaM = new SHA512Managed())
            {
                result = shaM.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashMe));
            }
#endif
            return result;
        }

		/// <summary>
		/// Get SHA512 hash for given string
		/// </summary>
		/// <param name="hashMe">this/input bytes</param>
		/// <returns>Hash Byte</returns>
		public static byte[] HashSHA512(byte[] hashMe)
		{
			byte[] result;

#if NETSTANDARD1_6
			using (SHA512 sha = SHA512.Create())
			{
				result = sha.ComputeHash(hashMe);
			}
#else
            using (SHA512 shaM = new SHA512Managed())
            {
                result = shaM.ComputeHash(hashMe);
            }
#endif
			return result;
		}
	}
}