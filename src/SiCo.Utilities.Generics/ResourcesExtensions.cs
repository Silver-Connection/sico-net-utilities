namespace SiCo.Utilities.Generics
{
    using System;
    using System.IO;
    using System.Reflection;

    ///<Summary>
    /// Helpers for Resources
    ///</Summary>
    public class ResourcesExtensions
    {
#if NETSTANDARD1_6 || NETSTANDARD2_0

        /// <summary>
        /// Concatenate file content
        /// </summary>
        /// <param name="assembly">Type of assembly to use</param>
        /// <param name="args">Files</param>
        /// <returns>Result string</returns>
        public static string ConcatFile(Type assembly, params string[] args)
        {
            Assembly assemblyDB = assembly.GetTypeInfo().Assembly;
            return ConcatFileBase(assemblyDB, args);
        }

#else

        /// <summary>
        /// Concatenate file content
        /// </summary>
        /// <param name="args">Files</param>
        /// <returns>Result string</returns>
        public static string ConcatFile(params string[] args)
        {
            Assembly assemblyDB = Assembly.GetExecutingAssembly();
            return ConcatFileBase(assemblyDB, args);
        }

#endif

        private static string ConcatFileBase(Assembly assemblyDB, params string[] args)
        {
            string result = string.Empty;
            foreach (var item in args)
            {
                using (Stream file = assemblyDB.GetManifestResourceStream(item))
                {
                    using (StreamReader sr = new StreamReader(file))
                    {
                        result += sr.ReadToEnd();
                    }
                }
            }

            return result;
        }
    }
}