namespace SiCo.Utilities.Compression
{
    /// <summary>
    /// GZip Helper
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// Compress with GZip to file-system
        /// </summary>
        /// <param name="file">Filename </param>
        /// <returns>Filename of archive</returns>
        public static string Compress(string file)
        {
            return Generics.GZip.Compress(file);
        }

        /// <summary>
        /// Compress with GZip
        /// </summary>
        /// <param name="raw">RAW bytes</param>
        /// <returns>Compressed bytes</returns>
        public static byte[] Compress(byte[] raw)
        {
            return Generics.GZip.Compress(raw);
        }

        /// <summary>
        /// Decompress with GZip to file-system
        /// </summary>
        /// <param name="file">Filename </param>
        /// <returns>Filename of extracted archive</returns>
        public static string Decompress(string file)
        {
            return Generics.GZip.Decompress(file);
        }

        /// <summary>
        /// Decompress with GZip
        /// </summary>
        /// <param name="gzip">GZIP bytes</param>
        /// <returns>Extracted bytes</returns>
        public static byte[] Decompress(byte[] gzip)
        {
            return Generics.GZip.Decompress(gzip);
        }
    }
}