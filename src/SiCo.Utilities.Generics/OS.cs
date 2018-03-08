namespace SiCo.Utilities.Generics
{
    using System;
    using System.IO;
    using System.Runtime.InteropServices;

    ///<Summary>
    /// Helper for determining the OS on which this code runs
    ///</Summary>
    public static class OS
    {
        private static bool isLinux;
        private static bool isMacOS;
        private static bool isWindows;

        static OS()
        {
            string windir = Environment.GetEnvironmentVariable("windir");
            if (!string.IsNullOrEmpty(windir) && windir.Contains(@"\") && Directory.Exists(windir))
            {
                isWindows = true;
            }
            else if (File.Exists(@"/proc/sys/kernel/ostype"))
            {
                string osType = File.ReadAllText(@"/proc/sys/kernel/ostype");
                if (osType.StartsWith("Linux", StringComparison.OrdinalIgnoreCase))
                {
                    // Note: Android gets here too
                    isLinux = true;
                }
                else
                {
                    throw new Exception("Can not detect platform");
                }
            }
            else if (File.Exists(@"/System/Library/CoreServices/SystemVersion.plist"))
            {
                // Note: iOS gets here too
                isMacOS = true;
            }
            else
            {
                throw new Exception("Can not detect platform");
            }
        }

        ///<Summary>
        /// Linux OS
        ///</Summary>
        public static bool IsLinux
        {
            get
            {
                return isLinux;
            }
        }

        ///<Summary>
        /// Mac OS
        ///</Summary>
        public static bool IsMacOS
        {
            get
            {
                return isMacOS;
            }
        }

        ///<Summary>
        /// Windows OS
        ///</Summary>
        public static bool IsWindows
        {
            get
            {
                return isWindows;
            }
        }

        ///<Summary>
        /// Detect platform
        ///</Summary>
        public static OSPlatform Platform()
        {
            if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
            {
                return OSPlatform.Windows;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
            {
                return OSPlatform.Linux;
            }

            if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
            {
                return OSPlatform.OSX;
            }

            throw new Exception("Can not detect platform");
        }
    }
}