namespace SiCo.Utilities.Generics.Test
{
    using System;
    using System.Collections.Generic;
    using Generics;
    using Xunit;

    public class DirFileTest
    {
        [PlatformFact(TestPlatforms.Windows)]
        public void Combine_Windows()
        {
            var tests = new List<Tuple<string, string, string>>(){
                new Tuple<string,string,string>("C:\\Tmp\\", "\\File.txt", "C:\\Tmp\\File.txt"),
                new Tuple<string,string,string>("C:\\Tmp\\", "File.txt", "C:\\Tmp\\File.txt"),
                new Tuple<string,string,string>("C:\\Tmp", "\\File.txt", "C:\\Tmp\\File.txt"),
                new Tuple<string,string,string>("C:\\Tmp", "File.txt", "C:\\Tmp\\File.txt"),
            };

            foreach (var test in tests)
            {
                var c = DirFile.Combine(test.Item1, test.Item2);
                Assert.Equal(test.Item3, c);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void Combine_Linux()
        {
            var tests = new List<Tuple<string, string, string>>(){
                new Tuple<string,string,string>("/var/log/", "/sample.log", "/var/log/sample.log"),
                new Tuple<string,string,string>("/var/log", "/sample.log", "/var/log/sample.log"),
                new Tuple<string,string,string>("/var/log/", "sample.log", "/var/log/sample.log"),
                new Tuple<string,string,string>("/var/log", "sample.log", "/var/log/sample.log"),
            };

            foreach (var test in tests)
            {
                var c = DirFile.Combine(test.Item1, test.Item2);
                Assert.Equal(test.Item3, c);
            }
        }

        [PlatformFact(TestPlatforms.Windows)]
        public void Split_Windows()
        {
            var tests = new List<Tuple<string, string, string>>(){
                new Tuple<string,string,string>("C:\\Tmp\\File.txt", "C:\\Tmp\\", "File.txt"),
                new Tuple<string,string,string>("C:\\Tmp\\File", "C:\\Tmp\\", "File"),
                new Tuple<string,string,string>("C:\\Tmp\\..\\File.txt", "C:\\Tmp\\..\\", "File.txt"),
                new Tuple<string,string,string>("C:\\Tmp\\Dir\\", "C:\\Tmp\\Dir\\", string.Empty),
            };

            foreach (var test in tests)
            {
                var c = DirFile.Split(test.Item1);
                Assert.Equal(test.Item2, c.Item1);
                Assert.Equal(test.Item3, c.Item2);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void Split_Linux()
        {
            var tests = new List<Tuple<string, string, string>>(){
                new Tuple<string,string,string>("/var/log/sample.log", "/var/log/", "sample.log" ),
                new Tuple<string,string,string>("/sample/log", "/sample/", "log" ),
                new Tuple<string,string,string>("/var/log/../sample.log", "/var/log/../", "sample.log" ),
                new Tuple<string,string,string>("/sample/log/", "/sample/log/", string.Empty ),
            };

            foreach (var test in tests)
            {
                var c = DirFile.Split(test.Item1);
                Assert.Equal(test.Item2, c.Item1);
                Assert.Equal(test.Item3, c.Item2);
            }
        }

        [PlatformFact(TestPlatforms.Windows)]
        public void TrimFile_Windows()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>(".\\File.txt", "File.txt"),
                new Tuple<string,string>("\\File.txt", "File.txt"),
                new Tuple<string,string>("File.txt", "File.txt"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimFile();
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void TrimFile_Linux()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("./File.txt", "File.txt"),
                new Tuple<string,string>("File.txt", "File.txt"),
                new Tuple<string,string>("../File.txt", "../File.txt"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimFile();
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.Windows)]
        public void TrimFileSet_Windows()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>(".\\File.txt", "\\File.txt"),
                new Tuple<string,string>("\\File.txt", "\\File.txt"),
                new Tuple<string,string>("File.txt", "\\File.txt"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimFile(true);
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void TrimFileSet_Linux()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("./File.txt", "/File.txt"),
                new Tuple<string,string>("File.txt", "/File.txt"),
                new Tuple<string,string>("../File.txt", "/../File.txt"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimFile(true);
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.Windows)]
        public void TrimPath_Windows()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("C:\\Tmp\\", "C:\\Tmp"),
                new Tuple<string,string>("C:\\Tmp", "C:\\Tmp"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimPath();
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void TrimPath_Linux()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("/tmp/", "/tmp"),
                new Tuple<string,string>("./tmp/", "tmp"),
                new Tuple<string,string>("../tmp", "../tmp"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimPath();
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.Windows)]
        public void TrimPathSet_Windows()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("C:\\Tmp\\", "C:\\Tmp\\"),
                new Tuple<string,string>("C:\\Tmp", "C:\\Tmp\\"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimPath(true);
                Assert.Equal(test.Item2, c);
            }
        }

        [PlatformFact(TestPlatforms.AnyUnix)]
        public void TrimPathSet_Linux()
        {
            var tests = new List<Tuple<string, string>>(){
                new Tuple<string,string>("/tmp/", "/tmp/"),
                new Tuple<string,string>("./tmp/", "tmp/"),
                new Tuple<string,string>("../tmp", "../tmp/"),
            };

            foreach (var test in tests)
            {
                var c = test.Item1.TrimPath(true);
                Assert.Equal(test.Item2, c);
            }
        }
    }
}