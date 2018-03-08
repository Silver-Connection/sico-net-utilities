namespace SiCo.Utilities.Crypto
{
    using SiCo.Utilities.Generics;

    /// <summary>
    /// Update encrypted IDs
    /// </summary>
    public static class UpdateExtensions
    {
        /// <summary>
        /// Update encrypted ID
        /// </summary>
        /// <param name="value">ID Property</param>
        /// <param name="update">New Value</param>
        /// <param name="save">REF True if ID is changed</param>
        /// <returns>ID</returns>
        public static int UpdateId(this int value, string update, ref bool save)
        {
            if (StringExtensions.IsEmpty(update))
            {
                return 0;
            }

            var id = TripleDES.DecryptIDZero(update);

            if (value != id)
            {
                save = true;
                value = id;
            }

            return value;
        }

        /// <summary>
        /// Update encrypted ID
        /// </summary>
        /// <param name="value">ID Property</param>
        /// <param name="update">New Value</param>
        /// <param name="save">REF True if ID is changed</param>
        /// <returns>ID</returns>
        public static long UpdateId(this long value, string update, ref bool save)
        {
            if (StringExtensions.IsEmpty(update))
            {
                return 0;
            }

            var id = TripleDES.DecryptIDZero(update);

            if (value != id)
            {
                save = true;
                value = id;
            }

            return value;
        }

        /// <summary>
        /// Update encrypted ID
        /// </summary>
        /// <param name="value">ID Property</param>
        /// <param name="update">New Value</param>
        /// <param name="save">REF True if ID is changed</param>
        /// <returns>ID</returns>
        public static int? UpdateId(this int? value, string update, ref bool save)
        {
            if (StringExtensions.IsEmpty(update))
            {
                return null;
            }

            var id = TripleDES.DecryptIDNull(update);

            if (value != id)
            {
                save = true;
                value = id;
            }

            return value;
        }

        /// <summary>
        /// Update encrypted ID
        /// </summary>
        /// <param name="value">ID Property</param>
        /// <param name="update">New Value</param>
        /// <param name="save">REF True if ID is changed</param>
        /// <returns>ID</returns>
        public static long? UpdateId(this long? value, string update, ref bool save)
        {
            if (StringExtensions.IsEmpty(update))
            {
                return null;
            }

            var id = TripleDES.DecryptIDNull(update);

            if (value != id)
            {
                save = true;
                value = id;
            }

            return value;
        }
    }
}