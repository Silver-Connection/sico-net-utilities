namespace SiCo.Utilities.Helper.Models
{
    using System;

    /// <summary>
    /// Timezone Model
    /// </summary>
    public class TimeZoneModel
    {
        /// <summary>
        /// Init
        /// </summary>
        public TimeZoneModel()
        {
            this.DateTime = DateTime.UtcNow;
            this.Human = string.Empty;
            this.Territory = string.Empty;
            this.Type = string.Empty;
        }

        /// <summary>
        /// Init
        /// </summary>
        /// <param name="datetime"></param>
        /// <param name="human"></param>
        /// <param name="type"></param>
        public TimeZoneModel(DateTime datetime, string human, string type)
        {
            this.DateTime = datetime;
            this.Human = human;
            this.Type = type;
        }

        /// <summary>
        /// Date Time in this Timezone
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Date Time in human readable
        /// </summary>
        public string Human { get; set; }

        /// <summary>
        /// Name of Territory
        /// </summary>
        public string Territory { get; set; }

        /// <summary>
        /// Timezone Type
        /// </summary>
        public string Type { get; set; }
    }
}