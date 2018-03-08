namespace SiCo.Utilities.Generics
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;

    /// <summary>
    /// Directory and file helpers
    /// </summary>
    public static class DirFile
    {
        /// <summary>
        /// Combine path and file name to one string.
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="file">Filename</param>
        /// <returns>Full path</returns>
        public static string Combine(string path, string file)
        {
            if (StringExtensions.IsEmpty(path))
            {
                if (StringExtensions.IsEmpty(file))
                {
                    return string.Empty;
                }

                return file.TrimPath();
            }

            if (StringExtensions.IsEmpty(file))
            {
                if (StringExtensions.IsEmpty(path))
                {
                    return string.Empty;
                }

                return path.TrimPath();
            }

            return Path.Combine(path.TrimPath(), file.TrimFile());
        }

        /// <summary>
        /// List all files in a given path.
        /// </summary>
        /// <param name="path">Path</param>
        /// <param name="filter">Filter</param>
        /// <returns>List of all filenames</returns>
        public static IEnumerable<string> ListFiles(string path, string filter)
        {
            if (StringExtensions.IsEmpty(path))
            {
                return null;
            }

            if (!System.IO.Directory.Exists(path))
            {
                return null;
            }

            return System.IO.Directory.GetFiles(path, string.IsNullOrEmpty(filter) ? "*" : filter).Select(i => Path.GetFileName(i));
        }

        /// <summary>
        /// Split path in file and path parts.
        /// Item1: Path
        /// Item2: File
        /// </summary>
        /// <param name="path">Path</param>
        /// <returns>Item1: Path and Item2: File</returns>
        public static Tuple<string, string> Split(string path)
        {
            if (StringExtensions.IsEmpty(path))
            {
                return null;
            }

            if (path.Last() == Path.DirectorySeparatorChar)
            {
                return new Tuple<string, string>(path, string.Empty);
            }

            var p_path = path.Substring(0, path.LastIndexOf(Path.DirectorySeparatorChar) + 1);
            var p_file = path.Substring(path.LastIndexOf(Path.DirectorySeparatorChar) + 1);

            return new Tuple<string, string>(p_path, p_file);
        }

        /// <summary>
        /// Trim first path separator. Return empty string if input is empty.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>Trim File</returns>
        public static string TrimFile(this string inString)
        {
            if (!StringExtensions.IsEmpty(inString))
            {
                inString = inString.Trim();

                // Check if relative path
                if (inString.Substring(0, 2) == "..")
                {
                    return inString;
                }
                else
                {
                    return inString.TrimFirst(".").TrimStart(Path.DirectorySeparatorChar);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Trim first path separator. Return empty string if input is empty.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="set">Set first path separator</param>
        /// <returns>Trim File</returns>
        public static string TrimFile(this string inString, bool set)
        {
            if (!StringExtensions.IsEmpty(inString))
            {
                inString = inString.Trim();
                var re = (set ? Path.DirectorySeparatorChar.ToString() : string.Empty);

                // Check if relative path
                if (inString.Substring(0, 2) == "..")
                {
                    return re + inString;
                }
                else
                {
                    return re + inString.TrimFirst(".").TrimStart(Path.DirectorySeparatorChar);
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Trim last path separator. Return empty string if input is empty.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <returns>Trim Path</returns>
        public static string TrimPath(this string inString)
        {
            if (!StringExtensions.IsEmpty(inString))
            {
                inString = inString.Trim();

                // Check if relative path
                if (inString.Substring(0, 2) == "./")
                {
                    inString = inString.TrimFirst(".").TrimStart(Path.DirectorySeparatorChar); ;
                }
                return inString.TrimEnd(Path.DirectorySeparatorChar);
            }

            return string.Empty;
        }

        /// <summary>
        /// Trim last path separator. Return empty string if input is empty.
        /// </summary>
        /// <param name="inString">this/input string</param>
        /// <param name="set">Set last path separator</param>
        /// <returns>Trim Path</returns>
        public static string TrimPath(this string inString, bool set)
        {
            if (!StringExtensions.IsEmpty(inString))
            {
                inString = inString.Trim();
                var re = (set ? Path.DirectorySeparatorChar.ToString() : string.Empty);

                // Check if relative path
                if (inString.Substring(0, 2) == "./")
                {
                    inString = inString.TrimFirst(".").TrimStart(Path.DirectorySeparatorChar); ;
                }

                return inString.TrimEnd(Path.DirectorySeparatorChar) + re;
            }

            return string.Empty;
        }
    }
}