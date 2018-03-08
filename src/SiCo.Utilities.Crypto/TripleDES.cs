namespace SiCo.Utilities.Crypto
{
    using System;
    using System.Text;
    using System.IO;
    using System.Security.Cryptography;
    using Generics;

    /// <summary>
    /// 3DES Helper
    /// </summary>
    public sealed class TripleDES
    {
        // Fields
        private static byte[] iv192 = new byte[]
        {
            0x32, 0x69, 0xf6, 0xaf, 0x24, 0x63, 0xa7, 0x2b, 0x2a, 5, 0x3e, 0x53, 0xb8, 7, 0xd1, 13,
            0x91, 0xe7, 200, 0x3a, 0xfd, 10, 0x79, 0xde
        };

        private static byte[] iv64 = new byte[]
        {
            0x37, 0x71, 0xf6, 0x4f, 0x24, 0x63, 0xa7, 3
        };

        private static byte[] key192 = new byte[]
        {
            0x2a, 0x10, 0x5d, 0x9c, 0x4e, 4, 0xda, 0x20, 15, 0xa7, 0x2c, 80, 0x1a, 250, 0x9b, 0x70,
            2, 0x5e, 11, 0xcc, 0x77, 0x23, 0xb8, 0xc5
        };

        private static byte[] key64 = new byte[]
        {
            0x2a, 0x12, 0x5d, 0x9c, 0x4e, 4, 0xda, 0x20
        };

        private static byte[] key = "6152da08f93e819fbd26fce0785b0eec3d0cb6bf7c53c505".HexToByteArray();

        private static byte[] iv = "8fc62ae5e7f28cde".HexToByteArray();

        /// <summary>
        /// Set Key
        /// IMPORTANT: Please set a custom key for your application which is not public 
        /// </summary>
        /// <param name="keyString"></param>
        public static void SetKey(string keyString)
        {
            key = keyString.HexToByteArray();
        }

        /// <summary>
        /// Decrypt encrypted string
        /// </summary>
        /// <remarks>
        /// Throws an exception if value is null or decrypting fails
        /// </remarks>
        /// <param name="encrypted">Encrypted Text</param>
        /// <returns>String</returns>
        public static string Decrypt(string encrypted)
        {
            if (StringExtensions.IsEmpty(encrypted))
            {
                throw new ArgumentNullException("Input value is empty!");
            }

            encrypted = encrypted.Replace("(sl)", "/");
            encrypted = encrypted.Replace("(eq)", "=");
            encrypted = encrypted.Replace("(p)", "+");

            try
            {
                using (
#if NETSTANDARD1_6
                        var provider = System.Security.Cryptography.TripleDES.Create()
#else
                        var provider = new TripleDESCryptoServiceProvider()
#endif
                        )
                {
                    using (MemoryStream stream = new MemoryStream(Convert.FromBase64String(encrypted)))
                    {
                        provider.Key = key;
                        provider.IV = iv;
                        provider.Padding = PaddingMode.PKCS7;
                        provider.Mode = CipherMode.CBC;

                        using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateDecryptor(), CryptoStreamMode.Read))
                        {
                            using (StreamReader reader = new StreamReader(stream2))
                            {
                                return reader.ReadToEnd();
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Could not decrypt: {encrypted}", e);
            }
        }

        /// <summary>
        /// Decrypt string and return a number. If result is 0 (zero) an exception will raise.
        /// </summary>
        /// <param name="encrypted">Encrypted text</param>
        /// <returns>ID</returns>
        public static int DecryptID(string encrypted)
        {
            var id = int.Parse(Decrypt(encrypted));
            if (id == 0)
            {
                throw new ArgumentNullException("Decryption failed given ID is 0");
            }

            return id;
        }

        /// <summary>
        /// Decrypt string and return a number. If decrypting fails then function will return 0 (zero)
        /// </summary>
        /// <param name="encrypted">Encrypted text</param>
        /// <returns>ID</returns>
        public static int DecryptIDZero(string encrypted)
        {
            try
            {
                var id = int.Parse(Decrypt(encrypted));
                return id;
            }
            catch (Exception)
            {
                return 0;
            }
        }

        /// <summary>
        /// Decrypt string and return a number. If decrypting fails or ID is 0 (zero) then function will return null
        /// </summary>
        /// <param name="encrypted">Encrypted text</param>
        /// <returns>ID</returns>
        public static int? DecryptIDNull(string encrypted)
        {
            if (string.IsNullOrEmpty(encrypted) || string.IsNullOrWhiteSpace(encrypted))
            {
                return null;
            }

            try
            {
                var id = int.Parse(Decrypt(encrypted));
                return id == 0 ? null : (int?)id;
            }
            catch (Exception)
            {
                return null;
            }
        }

        /// <summary>
        /// Encrypt object. Object will be converted to string and then it will encrypted.
        /// </summary>
        /// <param name="objenc">Value to encrypt</param>
        /// <returns>Encrypted text</returns>
        public static string Encrypt(object objenc)
        {
            string decrypted = string.Empty;
            if (objenc == null)
            {
                throw new ArgumentNullException("Input value is empty!");
            }

            int? num;
            uint? numu;
            long? numbig;
            string text;
            Guid? guid;

            if ((num = objenc as int?) != null
                || (numu = objenc as uint?) != null
                || (numbig = objenc as long?) != null)
            {
                decrypted = objenc.ToString();
            }
            else if ((text = objenc as string) != null)
            {
                decrypted = text;
            }
            else if ((guid = objenc as Guid?) != null)
            {
                decrypted = guid.ToString();
            }
            else
            {
                throw new NotImplementedException("Unknown Data Type!");
            }

            if (string.IsNullOrEmpty(decrypted))
            {
                throw new ArgumentNullException("Input value is empty!");
            }

            try
            {
                using (
#if NETSTANDARD1_6
                        var provider = System.Security.Cryptography.TripleDES.Create()
#else
                        var provider = new TripleDESCryptoServiceProvider()
#endif
                        )
                {
                    using (MemoryStream stream = new MemoryStream())
                    {
                        provider.Key = key;
                        provider.IV = iv;
                        provider.Padding = PaddingMode.PKCS7;
                        provider.Mode = CipherMode.CBC;

                        using (CryptoStream stream2 = new CryptoStream(stream, provider.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            using (StreamWriter writer = new StreamWriter(stream2))
                            {
                                writer.Write(decrypted);
                                writer.Flush();
                                stream2.FlushFinalBlock();
                                stream.Flush();

                                string save = Convert.ToBase64String(stream.ToArray(), 0, (int)stream.Length)
                                    .Replace("/", "(sl)")
                                    .Replace("=", "(eq)")
                                    .Replace("+", "(p)");

                                return save;
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception($"Failed encrypting value: {decrypted}", e);
            }
        }
    }
}