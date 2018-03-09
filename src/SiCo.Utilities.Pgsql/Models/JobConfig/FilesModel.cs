namespace SiCo.Utilities.Pgsql.Models.JobConfig
{
    using System;
    using System.IO;
    using System.Security.Cryptography;
    using Newtonsoft.Json;

    /// <summary>
    /// File Result Model
    /// </summary>
    public class FilesModel
    {
        /// <summary>
        /// Constructor
        /// </summary>
        public FilesModel()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="filePath">Path to HASH</param>
        /// <param name="resultModel"></param>
        public FilesModel(string filePath, Common.ProcessResultModel resultModel)
        {
            this.File = Path.GetFileName(filePath);
            this.Checksum = Hash(filePath);

            if (resultModel != null)
            {
                this.Created = DateTime.Now;
                this.Duration = resultModel.Duartion.TotalMilliseconds;
                if (!string.IsNullOrEmpty(resultModel.Ouput) && !string.IsNullOrWhiteSpace(resultModel.Ouput))
                {
                    this.Output = resultModel.Ouput;
                }

                if (!string.IsNullOrEmpty(resultModel.Error) && !string.IsNullOrWhiteSpace(resultModel.Error))
                {
                    this.Error = resultModel.Error;
                }
            }
        }

        /// <summary>
        /// Created on
        /// </summary>
        [JsonProperty(Order = 10)]
        public DateTime Created { get; set; }

        /// <summary>
        /// Duration
        /// </summary>
        [JsonProperty(Order = 30)]
        public double Duration { get; set; }

        /// <summary>
        /// File Name
        /// </summary>
        [JsonProperty(Order = 2)]
        public string File { get; set; }

        /// <summary>
        /// Hash
        /// </summary>
        [JsonProperty(Order = 40)]
        public string Checksum { get; set; }

        /// <summary>
        /// Std Output
        /// </summary>
        [JsonProperty(Order = 50, NullValueHandling = NullValueHandling.Ignore)]
        public string Output { get; set; }

        /// <summary>
        /// Std Error
        /// </summary>
        [JsonProperty(Order = 60, NullValueHandling = NullValueHandling.Ignore)]
        public string Error { get; set; }

        /// <summary>
        /// Calculate hash value for given file, we are using SHA256
        /// </summary>
        /// <param name="filename">Filename</param>
        /// <returns>Hash string</returns>
        public static string Hash(string filename)
        {
            if (!System.IO.File.Exists(filename))
            {
                return "FILE NOT FOUND";
            }

            string sha256 = string.Empty;
            try
            {
                using (FileStream stream = System.IO.File.OpenRead(filename))
                {
                    using (var sha = SHA256.Create())
                    {
                        byte[] checksum = sha.ComputeHash(stream);
                        sha256 = BitConverter.ToString(checksum).Replace("-", string.Empty);
                    }
                }
            }
            catch (Exception e)
            {
                sha256 = e.Message;
            }

            return sha256;
        }
    }
}