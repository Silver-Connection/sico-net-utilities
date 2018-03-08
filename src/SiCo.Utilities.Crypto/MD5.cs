namespace SiCo.Utilities.Crypto
{
    /// <summary>
    /// MD5 Helper
    /// </summary>
    public static class MD5
    {
        /// <summary>
        /// Get MD5 hash for given string
        /// </summary>
        /// <param name="hashMe">this/input string</param>
        /// <returns>Hash Byte</returns>
        public static byte[] HashMD5(string hashMe)
        {
            byte[] result;
            using (var md5 = System.Security.Cryptography.MD5.Create())
            {
                result = md5.ComputeHash(System.Text.Encoding.UTF8.GetBytes(hashMe));
            }

            return result;
        }
    }
}