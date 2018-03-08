namespace SiCo.Utilities.Generics
{
    using System.IO;
    using System.IO.Compression;

    /// <summary>
    /// GZip Helpers
    /// </summary>
    public static class GZip
    {
        /// <summary>
        /// Compress with GZip to file-system
        /// </summary>
        /// <param name="file">Filename</param>
        /// <returns>Filename of archive</returns>
        public static string Compress(string file)
        {
            var fileToCompress = OpenFile(file);
            if (fileToCompress == null)
            {
                return string.Empty;
            }

            using (var originalFileStream = fileToCompress.OpenRead())
            {
                if ((File.GetAttributes(fileToCompress.FullName) & FileAttributes.Hidden) != FileAttributes.Hidden & fileToCompress.Extension != ".gz")
                {
                    using (var compressedFileStream = File.Create($"{fileToCompress.FullName}.gz"))
                    {
                        using (var compressionStream = new GZipStream(compressedFileStream, CompressionMode.Compress))
                        {
                            originalFileStream.CopyTo(compressionStream);
                            return $"{fileToCompress.FullName}.gz";
                        }
                    }
                }

                return string.Empty;
            }
        }

        /// <summary>
        /// Compress with GZip
        /// </summary>
        /// <param name="raw">RAW bytes</param>
        /// <returns>GZIP bytes</returns>
        public static byte[] Compress(byte[] raw)
        {
            if (raw == null)
            {
                return null;
            }

            using (var memory = new MemoryStream())
            {
                using (var gzip = new GZipStream(memory, CompressionMode.Compress))
                {
                    gzip.Write(raw, 0, raw.Length);
                }

                return memory.ToArray();
            }
        }

        /// <summary>
        /// Decompress with GZip to file-system
        /// </summary>
        /// <param name="file">Filename</param>
        /// <returns>Path to extracted files</returns>
        public static string Decompress(string file)
        {
            var fileToDecompress = OpenFile(file);
            if (fileToDecompress == null)
            {
                return string.Empty;
            }

            using (var originalFileStream = fileToDecompress.OpenRead())
            {
                string currentFileName = fileToDecompress.FullName;
                string newFileName = currentFileName.Remove(currentFileName.Length - fileToDecompress.Extension.Length);

                using (var decompressedFileStream = File.Create(newFileName))
                {
                    using (var decompressionStream = new GZipStream(originalFileStream, CompressionMode.Decompress))
                    {
                        decompressionStream.CopyTo(decompressedFileStream);
                        return newFileName;
                    }
                }
            }
        }

        /// <summary>
        /// Decompress with GZip
        /// </summary>
        /// <param name="gzip">GZIP bytes</param>
        /// <returns>RAW bytes</returns>
        public static byte[] Decompress(byte[] gzip)
        {
            if (gzip == null)
            {
                return null;
            }

            using (var stream = new GZipStream(new MemoryStream(gzip), CompressionMode.Decompress))
            {
                const int Size = 4096;
                byte[] buffer = new byte[Size];
                using (var memory = new MemoryStream())
                {
                    int count = 0;
                    do
                    {
                        count = stream.Read(buffer, 0, Size);
                        if (count > 0)
                        {
                            memory.Write(buffer, 0, count);
                        }
                    }
                    while (count > 0);
                    return memory.ToArray();
                }
            }
        }

        private static FileInfo OpenFile(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }

            return new FileInfo(file);
        }
    }
}