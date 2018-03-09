namespace SiCo.Utilities.Helper
{
    using System;
    using System.IO;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Newtonsoft.Json;

    /// <summary>
    /// Very simple logger which logs to a file
    /// </summary>
    public static class FileLogger
    {
        private static string file;

        static FileLogger()
        {
            if (!string.IsNullOrEmpty(file))
            {
                return;
            }

            if (Generics.OS.IsWindows)
            {
                file = @"C:\Temp\FileLogger\Log.txt";
            }
            else
            {
                file = @"/tmp/filelogger/log.txt";
            }
        }

        /// <summary>
        /// Path to Log File
        /// Default Windows:
        /// C:\Temp\FileLogger\Log.txt
        /// Default *nix;
        /// /tmp/filelogger/log.txt
        /// </summary>
        public static string File
        {
            get
            {
                return file;
            }

            set
            {
                file = value;
            }
        }

        /// <summary>
        /// Writes an Exception to the log
        /// <param name="e">Exception</param>
        /// <param name="t">Class / Method type</param>
        /// </summary>
        public static void Error(Exception e, Type t)
        {
            Write(e.ToString(), t);
        }

        /// <summary>
        /// Writes an Error Message to the log
        /// <param name="msg">Message</param>
        /// <param name="t">Class / Method type</param>
        /// </summary>
        public static void Error(string msg, Type t)
        {
            Write(msg, t);
        }

        /// <summary>
        /// Log a Info Message
        /// <param name="msg">Message</param>
        /// <param name="t">Class / Method type</param>
        /// </summary>
        public static void Info(string msg, Type t)
        {
            Write(msg, t);
        }

        /// <summary>
        /// Serialize Object to json an log it.
        /// <param name="model">Object</param>
        /// <param name="t">Class / Method type</param>
        /// </summary>
        public static void Json(object model, Type t)
        {
            string msg = JsonConvert.SerializeObject(model, Formatting.Indented);
            Write(msg, t);
        }

        private static void Write(string txt, Type t)
        {
            if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
            {
                return;
            }

            txt = string.Format("{0} Type: {1} \n{2}", DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss"), t.ToString(), txt);

            try
            {
                if (!System.IO.File.Exists(File))
                {
                    var path = Generics.DirFile.Split(File);
                    System.IO.Directory.CreateDirectory(path.Item1);
                }

                System.IO.File.AppendAllText(File, txt + Environment.NewLine);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #region Async

        /// <summary>
        /// Writes an Exception to the log
        /// <param name="e">Exception</param>
        /// <param name="t">Class / Method type</param>
        /// <param name="cancellationToken"></param>
        /// </summary>
        public static async Task ErrorAsync(Exception e, Type t, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WriteAsync(e.ToString(), t, cancellationToken);
        }

        /// <summary>
        /// Writes an Error Message to the log
        /// <param name="msg">Message</param>
        /// <param name="t">Class / Method type</param>
        /// <param name="cancellationToken"></param>
        /// </summary>
        public static async Task ErrorAsync(string msg, Type t, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WriteAsync(msg, t, cancellationToken);
        }

        /// <summary>
        /// Log a Info Message
        /// <param name="msg">Message</param>
        /// <param name="t">Class / Method type</param>
        /// <param name="cancellationToken"></param>
        /// </summary>
        public static async Task InfoAsync(string msg, Type t, CancellationToken cancellationToken = default(CancellationToken))
        {
            await WriteAsync(msg, t, cancellationToken);
        }

        /// <summary>
        /// Serialize Object to json an log it.
        /// <param name="model">Object</param>
        /// <param name="t">Class / Method type</param>
        /// <param name="cancellationToken"></param>
        /// </summary>
        public static async Task JsonAsync(object model, Type t, CancellationToken cancellationToken = default(CancellationToken))
        {
            string msg = JsonConvert.SerializeObject(model, Formatting.Indented);
            await WriteAsync(msg, t, cancellationToken);
        }

        private static async Task WriteAsync(string txt, Type t, CancellationToken cancellationToken = default(CancellationToken))
        {
            if (string.IsNullOrEmpty(txt) || string.IsNullOrWhiteSpace(txt))
            {
                return;
            }

            txt = string.Format("{0} Type: {1} \n{2}", DateTime.UtcNow.ToString("dd.MM.yyyy HH:mm:ss"), t.ToString(), txt);

            try
            {
                if (!System.IO.File.Exists(File))
                {
                    var path = Generics.DirFile.Split(File);
                    System.IO.Directory.CreateDirectory(path.Item1);
                }

                using (var stream = new FileStream(File, FileMode.Append, FileAccess.Write, FileShare.None, bufferSize: 4096, useAsync: true))
                {
                    var encoded = Encoding.Unicode.GetBytes(txt);
                    await stream.WriteAsync(encoded, 0, encoded.Length);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion Async
    }
}