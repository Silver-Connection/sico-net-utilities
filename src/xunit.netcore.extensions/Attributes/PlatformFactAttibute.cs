namespace Xunit
{
    public class PlatformFactAttribute : FactAttribute
    {
        public PlatformFactAttribute(TestPlatforms platform)
        {
            switch (platform)
            {
                case TestPlatforms.Windows:
                    if (!OSDetect.IsWindows)
                    {
                        Skip = "Test only runs on Windows.";
                    }
                    break;

                case TestPlatforms.Linux:
                    if (!OSDetect.IsLinux)
                    {
                        Skip = "Test only runs on Linux.";
                    }
                    break;

                case TestPlatforms.OSX:
                case TestPlatforms.NetBSD:
                case TestPlatforms.FreeBSD:
                    if (!OSDetect.IsMacOS)
                    {
                        Skip = "Test only runs on MacOS / FreeBSD / NetBSD.";
                    }
                    break;

                case TestPlatforms.AnyUnix:
                    if (OSDetect.IsWindows)
                    {
                        Skip = "Test only runs on posix.";
                    }
                    break;

                case TestPlatforms.Any:
                default:
                    break;
            }
        }
    }
}