namespace SiCo.Utilities.Generics
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;

    /// <summary>
    /// Ip Helpers
    /// </summary>
    public static class IpHelper
    {
        /// <summary>
        /// Returns start and end IP address for a specified net-mask
        /// </summary>
        /// <param name="address">Address starting from</param>
        /// <param name="cidr">CIDR Mask-bit</param>
        /// <returns>IP address list</returns>
        public static IEnumerable<IPAddress> GetIpAdressList(string address, int cidr)
        {
            if (cidr >= 32)
            {
                return new IPAddress[] { IPAddress.Parse(address) };
            }

            var firstLast = GetIpRangeEdge(address, cidr);

            IPAddress ip_first = firstLast.First();
            IPAddress ip_last = firstLast.Last();

            var addresses = new List<IPAddress>
            {
                ip_first
            };

            int first = ConvertIPv4AddressToInt32(ip_first);
            int last = ConvertIPv4AddressToInt32(ip_last);

            while (first < last)
            {
                first++;
                addresses.Add(ConvertInt32ToIPv4Address(first));
            }

            return addresses;
        }

        /// <summary>
        /// Returns start and end IP address for a specified net-mask
        /// </summary>
        /// <param name="address">Address starting from</param>
        /// <param name="cidr">CIDR Mask-bit</param>
        /// <returns>IP address list</returns>
        public static IEnumerable<IPAddress> GetIpRangeEdge(string address, int cidr)
        {
            IPAddress ip = IPAddress.Parse(address);
            if (cidr >= 32)
            {
                return new IPAddress[] { ip, ip };
            }

            uint mask = ~(uint.MaxValue >> cidr);

            // Convert the IP addresses to bytes.
            byte[] bytes = ip.GetAddressBytes();

            // BitConverter gives bytes in opposite order to GetAddressBytes().
            byte[] maskBytes = BitConverter.GetBytes(mask).Reverse().ToArray();

            byte[] startIPBytes = new byte[bytes.Length];
            byte[] endIPBytes = new byte[bytes.Length];

            // Calculate the bytes of the start and end IP addresses.
            for (int i = 0; i < bytes.Length; i++)
            {
                startIPBytes[i] = (byte)(bytes[i] & maskBytes[i]);
                endIPBytes[i] = (byte)(bytes[i] | ~maskBytes[i]);
            }

            // Convert the bytes to IP addresses.
            IPAddress startIP = new IPAddress(startIPBytes);
            IPAddress endIP = new IPAddress(endIPBytes);

            return new IPAddress[] { startIP, endIP };
        }

        private static IPAddress ConvertInt32ToIPv4Address(int interger)
        {
            byte[] bytes = new byte[4];

            bytes[0] = (byte)((interger >> 24) & 0xFF);
            bytes[1] = (byte)((interger >> 16) & 0xFF);
            bytes[2] = (byte)((interger >> 8) & 0xFF);
            bytes[3] = (byte)(interger & 0xFF);

            return new IPAddress(bytes);
        }

        private static int ConvertIPv4AddressToInt32(IPAddress address)
        {
            byte[] bytes = address.GetAddressBytes();

            int interger = (((int)bytes[0]) << 24) + (((int)bytes[1]) << 16) + (((int)bytes[2]) << 8) + ((int)bytes[3]);

            return interger;
        }
    }
}