namespace SiCo.Utilities.Compression
{
    using System.IO;
    using SharpCompress.Archives;
    using SharpCompress.Archives.Tar;
    using SharpCompress.Readers;

    /// <summary>
    /// Tar Helper
    /// </summary>
    public static class Tar
    {
        /// <summary>
        /// Compress directory with Tar to file-system
        /// </summary>
        /// <param name="path">Path </param>
        /// <returns>Path to archive</returns>
        public static string Compress(string path)
        {
            if (Generics.StringExtensions.IsEmpty(path))
            {
                return string.Empty;
            }

            using (var archive = TarArchive.Create())
            {
                var gzip = new SharpCompress.Writers.WriterOptions(SharpCompress.Common.CompressionType.GZip);
                path = path.Trim().TrimEnd('.');
                archive.AddAllFromDirectory(path);
                archive.SaveTo(path + ".tar.gz", gzip);
                return path + ".tar.gz";
            }
        }

        /// <summary>
        /// Decompress with Tar to file-system
        /// </summary>
        /// <param name="file">Filename </param>
        /// <returns>Path to extracted files</returns>
        public static string Decompress(string file)
        {
            if (Generics.StringExtensions.IsEmpty(file))
            {
                return string.Empty;
            }

            using (var stream = File.OpenRead(file))
            using (var reader = ReaderFactory.Open(stream))
            {
                var dest = Common.OpenFile(file);
                var destPath = dest.FullName.Remove(dest.FullName.Length - dest.Extension.Length);

                while (reader.MoveToNextEntry())
                {
                    if (!reader.Entry.IsDirectory)
                    {
                        reader.WriteEntryToDirectory(destPath, new ExtractionOptions()
                        {
                            ExtractFullPath = true,
                            Overwrite = true
                        });
                    }
                }
                return destPath;
            }
        }
    }

}