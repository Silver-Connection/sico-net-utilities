namespace SiCo.Utilities.Helper
{
    using System.Collections.Generic;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Reflection;

    /// <summary>
    /// Country Helper Class
    /// </summary>
    public static class Countries
    {
        private static Dictionary<string, Models.CountryModel> countries = null;
        private static bool disposedValue = false;
#if !NETSTANDARD1_6
        private static Dictionary<string, RegionInfo> regions = null;
#endif

        /// <summary>
        /// Run first to read data
        /// </summary>
        static Countries()
        {
            Assembly assembly = typeof(Countries).GetTypeInfo().Assembly;
            using (Stream file = assembly.GetManifestResourceStream("SiCo.Utilities.Helper.Resources.ISOCountryCodes.txt"))
            {
                countries = new Dictionary<string, Models.CountryModel>();
                using (StreamReader sr = new StreamReader(file, System.Text.Encoding.UTF8))
                {
                    string line;
                    while (null != (line = sr.ReadLine()))
                    {
                        if (string.IsNullOrEmpty(line))
                        {
                            continue;
                        }

                        string iso2 = line.Substring(0, 2);
                        string iso3 = line.Substring(3, 3);
                        string name = line.Substring(7);

                        countries.Add(iso3, new Models.CountryModel(iso2, iso3, name));
                    }
                }
            }

            // Fill regions
#if !NETSTANDARD1_6
            regions = new Dictionary<string, RegionInfo>();
            foreach (CultureInfo ci in CultureInfo
                .GetCultures(CultureTypes.SpecificCultures)
                .Except(CultureInfo.GetCultures(CultureTypes.SpecificCultures)))
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                if (!regions.ContainsKey(ri.ThreeLetterISORegionName))
                {
                    regions.Add(ri.ThreeLetterISORegionName, ri);
                }
            }
#endif
        }

        /// <summary>
        /// List with countries
        /// </summary>
        public static Dictionary<string, Models.CountryModel> Data
        {
            get
            {
                return countries;
            }
        }

        /// <summary>
        /// Country count
        /// </summary>
        public static int Lenght
        {
            get
            {
                return countries.Count;
            }
        }

        #region Methods

        /// <summary>
        /// Convert ISOA2 -> ISOA3 and ISOA3 -> ISOA2
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>ISO code</returns>
        public static string ConvertIso(string iso3oriso2)
        {
            var country = GetCountryByIso(iso3oriso2);
            if (country != null)
            {
                if (iso3oriso2.Length == 3)
                {
                    return country.ISO2;
                }

                if (iso3oriso2.Length == 2)
                {
                    return country.ISO3;
                }
            }

            return string.Empty;
        }

        /// <summary>
        /// Returns a specific CountryModel from a given ISO (A2/A3) code
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>Country Model</returns>
        public static Models.CountryModel GetCountryByIso(string iso3oriso2)
        {
            if (string.IsNullOrEmpty(iso3oriso2) || string.IsNullOrWhiteSpace(iso3oriso2))
            {
                return null;
            }

            iso3oriso2 = iso3oriso2.ToUpper();
            if (iso3oriso2.Length == 3 && countries.Any(c => c.Key == iso3oriso2))
            {
                return countries.FirstOrDefault(c => c.Key == iso3oriso2).Value;
            }

            if (iso3oriso2.Length == 2 && countries.Any(c => c.Value.ISO2 == iso3oriso2))
            {
                return countries.FirstOrDefault(c => c.Value.ISO2 == iso3oriso2).Value;
            }

            return null;
        }

        /// <summary>
        /// Returns a specific CountryModel from a given ISO (A2/A3) code
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>Country Info Model</returns>
        public static Models.CountryInfoModel GetCountryInfoByIso(string iso3oriso2)
        {
            if (string.IsNullOrEmpty(iso3oriso2) || string.IsNullOrWhiteSpace(iso3oriso2))
            {
                return null;
            }

            var country = GetCountryByIso(iso3oriso2);
            if (country != null)
            {
                return new Models.CountryInfoModel(country);
            }

            return null;
        }

        /// <summary>
        /// Returns a specific CountryModel from a given country name
        /// </summary>
        /// <param name="name">Country name</param>
        /// <returns>Country Model</returns>
        public static Models.CountryModel GetCountryByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            name = name.ToUpper();
            if (countries.Any(c => c.Value.NameFallback.ToUpper() == name))
            {
                return countries.FirstOrDefault(c => c.Value.NameFallback.ToUpper() == name).Value;
            }

            return null;
        }

        /// <summary>
        /// Returns a specific CountryModel from a given country name
        /// </summary>
        /// <param name="name">Country ISO code</param>
        /// <returns>Country Model</returns>
        public static Models.CountryInfoModel GetCountryInfoByName(string name)
        {
            if (string.IsNullOrEmpty(name) || string.IsNullOrWhiteSpace(name))
            {
                return null;
            }

            var country = GetCountryByName(name);
            if (country != null)
            {
                return new Models.CountryInfoModel(country);
            }

            return null;
        }

        /// <summary>
        /// Returns a list of country ISO's
        /// </summary>
        /// <param name="isoA2">If true list will use ISOA2 codes instead of ISOA3</param>
        /// <returns>List of strings</returns>
        public static IEnumerable<string> GetIsoList(bool isoA2 = false)
        {
            foreach (var country in countries
                .Where(c => !string.IsNullOrEmpty(c.Value.NameFallback)
                    && !string.IsNullOrEmpty(c.Value.ISO3))
                .OrderBy(c => c.Value.NameFallback))
            {
                yield return isoA2 ? country.Value.ISO2 : country.Key;
            }
        }

        /// <summary>
        /// Returns a list of KeyValue, Key is ISO3 and Value is Country name
        /// </summary>
        /// <returns>List of KeyValuePair</returns>
        public static IEnumerable<KeyValuePair<string, string>> GetKeyValueList()
        {
            foreach (var country in countries
                .Where(c => !string.IsNullOrEmpty(c.Value.NameFallback)
                    && !string.IsNullOrEmpty(c.Value.ISO3))
                .OrderBy(c => c.Value.NameFallback))
            {
                yield return new KeyValuePair<string, string>(country.Value.ISO3, country.Value.NameFallback);
            }
        }

        /// <summary>
        /// Returns a list of countries
        /// </summary>
        /// <returns>List of KeyValuePair</returns>
        public static IEnumerable<Models.CountryInfoModel> GetList()
        {
            foreach (var country in countries
                .Where(c => !string.IsNullOrEmpty(c.Value.NameFallback)
                    && !string.IsNullOrEmpty(c.Value.ISO3))
                .OrderBy(c => c.Value.NameFallback))
            {
                yield return new Models.CountryInfoModel(country.Value);
            }
        }

        /// <summary>
        /// Get country name by ISO code.
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>Country name</returns>
        public static string GetName(string iso3oriso2)
        {
            var country = GetCountryByIso(iso3oriso2);
            return country == null ? string.Empty : country.NameFallback;
        }

        /// <summary>
        /// Check if ISO code is valid
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>boolean</returns>
        public static bool IsValid(string iso3oriso2)
        {
            if (string.IsNullOrEmpty(iso3oriso2) || string.IsNullOrWhiteSpace(iso3oriso2))
            {
                return false;
            }

            iso3oriso2 = iso3oriso2.Trim().ToUpper();
            if (iso3oriso2.Length == 3 && countries.Any(c => c.Key == iso3oriso2))
            {
                return true;
            }

            if (iso3oriso2.Length == 2 && countries.Any(c => c.Value.ISO2 == iso3oriso2))
            {
                return true;
            }

            return false;
        }

        #endregion Methods

        #region Disposable Support

        /// <summary>
        /// Dispose
        /// </summary>
        public static void Dispose()
        {
            Dispose(true);
        }

        private static void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                countries = null;
#if !NETSTANDARD1_6
                regions = null;
#endif
                disposedValue = true;
            }
        }

        #endregion Disposable Support
    }
}