namespace SiCo.Utilities.Compression
{
    using System.IO;

    internal class Common
    {
        internal static FileInfo OpenFile(string file)
        {
            if (!File.Exists(file))
            {
                return null;
            }

            return new FileInfo(file);
        }
    }
}