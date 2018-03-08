namespace SiCo.Utilities.Generics
{
    using System;
    using System.Globalization;
    using System.Text;

    /// <summary>
    /// Byte helpers
    /// </summary>
    public static class ByteExtensions
    {
        /// <summary>
        /// Convert ASCI to bytes
        /// </summary>
        /// <param name="s">ASCI string</param>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = bytes.AsciiBytes();
        /// ```
        /// </example>
        public static byte[] AsciiToBytes(string s)
        {
            byte[] bytes = new byte[s.Length];

            for (int i = 0; i < s.Length; i++)
            {
                bytes[i] = (byte)s[i];
            }

            return bytes;
        }

        /// <summary>
        /// Convert bytes to HEX string
        /// </summary>
        /// <param name="bytes">Bytes</param>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = bytes.ByteArrayToHex();
        /// ```
        /// </example>
        public static string ByteArrayToHex(this byte[] bytes)
        {
            StringBuilder builder = new StringBuilder(bytes.Length * 2);

            foreach (byte b in bytes)
            {
                builder.Append(b.ToString("X2"));
            }

            return builder.ToString();
        }

        /// <summary>
        /// Convert HEX string to bytes
        /// </summary>
        /// <param name="hexString">HEY string</param>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var bytes = st.HexToByteArray();
        /// ```
        /// </example>
        public static byte[] HexToByteArray(this string hexString)
        {
            byte[] bytes = new byte[hexString.Length / 2];

            for (int i = 0; i < hexString.Length; i += 2)
            {
                string s = hexString.Substring(i, 2);
                bytes[i / 2] = byte.Parse(s, NumberStyles.HexNumber, null);
            }

            return bytes;
        }

        /// <summary>
        /// Repeat byte
        /// </summary>
        /// <param name="b">Byte to repeat</param>
        /// <param name="count">Repeats</param>
        /// <returns>Byte</returns>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = bytes.RepeatByte(by, 10);
        /// ```
        /// </example>
        public static byte[] RepeatByte(byte b, int count)
        {
            byte[] value = new byte[count];

            for (int i = 0; i < count; i++)
            {
                value[i] = b;
            }

            return value;
        }

        /// <summary>
        /// Get Base64 string for byte
        /// </summary>
        /// <param name="i_byte">this/input data</param>
        /// <returns>string | string.Empty</returns>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = bytes.ToBase64();
        /// ```
        /// </example>
        public static string ToBase64(this byte[] i_byte)
        {
            if (i_byte != null)
            {
                return Convert.ToBase64String(i_byte);
            }

            return string.Empty;
        }

        /// <summary>
        /// Get string for byte
        /// </summary>
        /// <param name="i_byte">this/input data</param>
        /// <returns>string | string.Empty</returns>
        /// <example>
        /// ```csharp
        /// using SiCo.Utilities.Generics;
        /// ...
        /// var st = bytes.ToBase64();
        /// ```
        /// </example>
        public static string ToStringSafe(this byte[] i_byte)
        {
            if (i_byte != null)
            {
                return BitConverter.ToString(i_byte).Replace("-", string.Empty).ToLower();
            }

            return string.Empty;
        }
    }
}