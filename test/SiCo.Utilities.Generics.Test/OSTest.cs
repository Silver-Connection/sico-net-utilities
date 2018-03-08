namespace SiCo.Utilities.Generics.Test
{
    using Generics;
    using Xunit;

    public class OSTest
    {
        [PlatformFact(TestPlatforms.Windows)]
        public void DetectOS_Windows()
        {
            var os = OS.IsWindows;

            Assert.True(os);
        }

        [PlatformFact(TestPlatforms.Linux)]
        public void DetectOS_Linux()
        {
            var os = OS.IsLinux;

            Assert.True(os);
        }

        [PlatformFact(TestPlatforms.OSX)]
        public void DetectOS_OSX()
        {
            var os = OS.IsMacOS;

            Assert.True(os);
        }
    }
}
