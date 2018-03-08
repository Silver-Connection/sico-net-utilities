namespace SiCo.Utilities.Helper
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.InteropServices;

    /// <summary>
    /// Time Zones Helper
    /// </summary>
    public sealed class TimeZones
    {
        private static bool disposedValue = false;

        private static IEnumerable<Xml.TimeZoneItem> zones = null;

        /// <summary>
        /// Run first to read data
        /// </summary>
        static TimeZones()
        {
            // Fill timezones
            if (zones == null)
            {
                Assembly assembly = typeof(Countries).GetTypeInfo().Assembly;
                using (Stream file = assembly.GetManifestResourceStream("SiCo.Utilities.Helper.Resources.TimeZones.xml"))
                {
                    using (var fileInputStream = new StreamReader(file))
                    {
                        var serializer = new System.Xml.Serialization.XmlSerializer(typeof(Xml.TimeZonesDocument));
                        var model = (Xml.TimeZonesDocument)serializer.Deserialize(fileInputStream);
                        zones = model.Items;
                    }
                }
            }
        }

        private TimeZones()
        {
        }

        /// <summary>
        /// Time Zone Data
        /// </summary>
        public static IEnumerable<Xml.TimeZoneItem> Data
        {
            get
            {
                return zones;
            }
        }

        /// <summary>
        /// Timezones count
        /// </summary>
        public static int Lenght
        {
            get
            {
                if (zones != null)
                {
                    return zones.Count();
                }

                return 0;
            }
        }

        #region Methods

        /// <summary>
        /// Returns all timezones for a given country.
        /// </summary>
        /// <param name="iso3oriso2">Country ISO code</param>
        /// <returns>List of all Timezones for a given country</returns>
        public static IEnumerable<Models.TimeZoneModel> GetZonesByIso(string iso3oriso2)
        {
            if (iso3oriso2.Length == 3)
            {
                iso3oriso2 = Countries.ConvertIso(iso3oriso2);
            }

            var result = zones.Where(i => i.Territory == iso3oriso2);
            if (result == null || result.Count() <= 0)
            {
                return null;
            }

            var timezones = new List<Models.TimeZoneModel>();
            foreach (var tz in result)
            {
                DateTime dt = new DateTime(DateTime.UtcNow.Ticks);
                timezones.Add(new Models.TimeZoneModel()
                {
                    DateTime = TimeZoneInfo.ConvertTime(dt, TimeZoneInfo.FindSystemTimeZoneById(RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? tz.Other : tz.Type)),
                    Human = tz.Other,
                    Territory = tz.Territory,
                    Type = tz.Type,
                });
            }

            return timezones;
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
                zones = null;
                disposedValue = true;
            }
        }

        #endregion Disposable Support
    }
}