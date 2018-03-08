namespace SiCo.Utilities.Helper.Models
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Reflection;
    using Newtonsoft.Json;

    /// <summary>
    /// Country Model
    /// </summary>
    public class CountryModel
    {
        private byte[] flagData;
        private string flagMime;

        private string iso2;
        private string iso3;

        private string nameFallback;

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="iso2"></param>
        /// <param name="iso3"></param>
        /// <param name="name"></param>
        public CountryModel(string iso2, string iso3, string name)
        {
            this.iso2 = iso2;
            this.iso3 = iso3;
            this.nameFallback = name;

            // Load flag data
            this.flagMime = "image/svg+xml";

            Assembly assembly = typeof(CountryModel).GetTypeInfo().Assembly;
            using (Stream file = assembly.GetManifestResourceStream(string.Concat("SiCo.Utilities.Helper.Resources.Flags.", this.ISO3, ".svg")))
            {
                var content = file;

                // Load invisible file
                if (null == content)
                {
                    content = assembly.GetManifestResourceStream("SiCo.Utilities.Helper.Resources.Flags.invisible.svg");
                }

                this.flagData = new byte[content.Length];
                content.Read(this.flagData, 0, (int)content.Length);
                content = null;
            }
        }

        /// <summary>
        /// Flag Data
        /// </summary>
        [JsonIgnore]
        public byte[] FlagData
        {
            get
            {
                return this.flagData;
            }
        }

        /// <summary>
        /// Flag MIME type
        /// </summary>
        public string FlagMime
        {
            get
            {
                return this.flagMime;
            }
        }

        /// <summary>
        /// ISO A2 Country Code
        /// </summary>
        public string ISO2
        {
            get
            {
                return this.iso2;
            }
        }

        /// <summary>
        /// ISO A3 Country Code
        /// </summary>
        public string ISO3
        {
            get
            {
                return this.iso3;
            }
        }

        /// <summary>
        /// Country Name Fall-back
        /// </summary>
        public string NameFallback
        {
            get
            {
                return this.nameFallback;
            }
        }

        #region Methods

        /// <summary>
        /// Get Flag Data in base64 encoded string
        /// </summary>
        /// <returns></returns>
        public string GetFlagBase64()
        {
            if (this.flagData != null)
            {
                return "data:" + this.flagMime + ";base64, " + Convert.ToBase64String(this.flagData);
            }

            return string.Empty;
        }

        /// <summary>
        /// Get Timezones for this Country
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Models.TimeZoneModel> GetTimeZones()
        {
            return TimeZones.GetZonesByIso(this.ISO2);
        }

        #endregion Methods
    }
}