namespace SiCo.Utilities.Compression
{
    using System.IO;

    /// <summary>
    /// ZIP Helper
    /// </summary>
    public class Zip
    {
        /// <summary>
        /// Compress to a Zip file
        /// </summary>
        /// <param name="path">Directory where the Archive is located</param>
        /// <param name="archiveName">Name of the ZIP Archive</param>
        /// <returns>Compressed bytes</returns>
        public static void Compress(string path, string archiveName)
        {
            if (!Directory.Exists(path))
            {
                return;
            }

            System.IO.Compression.ZipFile.CreateFromDirectory(path, archiveName);
        }

        /// <summary>
        /// Extract Zip File
        /// </summary>
        /// <param name="file">Filename </param>
        /// <param name="destination">Destination Path</param>
        public static void Decompress(string file, string destination)
        {
            var fileInfo = OpenFile(file);
            if (fileInfo == null)
            {
                return;
            }

            System.IO.Compression.ZipFile.ExtractToDirectory(fileInfo.FullName, destination);
        }

        /// <summary>
        /// Extract Zip File
        /// </summary>
        /// <param name="file">Filename </param>
        /// <returns>Destination Path</returns>
        public static string Decompress(string file)
        {
            var fileInfo = OpenFile(file);
            if (fileInfo == null)
            {
                return string.Empty;
            }

            var destination = fileInfo.FullName.Replace(fileInfo.Extension, string.Empty);
            System.IO.Compression.ZipFile.ExtractToDirectory(fileInfo.FullName, destination);
            return destination;
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