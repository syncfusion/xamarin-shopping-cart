using System;
using System.IO;
using System.Net;
using System.Security.Cryptography;
using System.Text;

namespace ShoppingAPI.Service
{
    /// <summary>
    /// This class is responsible for the authentication service.
    /// </summary>
    public class AuthenticationService
    {
        /// <summary>
        /// Encrypt the text.
        /// </summary>
        /// <param name="encryptText"></param>
        /// <param name="encryptionKey"></param>
        /// <returns></returns>
        public static string Encrypt(string encryptText, string encryptionKey)
        {
            try
            {
                var clearBytes = Encoding.Unicode.GetBytes(encryptText);
                using (var encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(encryptionKey,
                        new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(clearBytes, 0, clearBytes.Length);
                            cs.Close();
                        }

                        encryptText = Convert.ToBase64String(ms.ToArray());
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return encryptText;
        }

        /// <summary>
        /// Decrypt the text.
        /// </summary>
        /// <param name="decryptText"></param>
        /// <param name="decryptionKey"></param>
        /// <returns></returns>
        public static string Decrypt(string decryptText, string decryptionKey)
        {
            try
            {
                decryptText = decryptText.Replace(" ", "+");
                var cipherBytes = Convert.FromBase64String(decryptText);
                using (var encryptor = Aes.Create())
                {
                    var pdb = new Rfc2898DeriveBytes(decryptionKey,
                        new byte[] {0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76});
                    encryptor.Key = pdb.GetBytes(32);
                    encryptor.IV = pdb.GetBytes(16);
                    using (var ms = new MemoryStream())
                    {
                        using (var cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                        {
                            cs.Write(cipherBytes, 0, cipherBytes.Length);
                            cs.Close();
                        }

                        decryptText = Encoding.Unicode.GetString(ms.ToArray());
                    }
                }
            }
            catch (WebException ex)
            {
                throw ex;
            }

            return decryptText;
        }
    }
}