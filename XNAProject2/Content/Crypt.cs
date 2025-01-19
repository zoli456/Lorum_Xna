using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Lórum.Content
{
    public class Encryptor
    {
        private static readonly string _Pwd = "VzRtH7lRlrrxMalr9JQh";
        //Be careful storing this in code unless it’s secured and not distributed

        private static readonly byte[] _Salt =
        {
            0x45, 0xF1, 0x61, 0x6e, 0x20, 0x00, 0x65, 0x64, 0x76, 0x65, 0x64,
            0x03, 0x76
        };

        internal static string Decrypt(string cipherText)
        {
            var cipherBytes = Convert.FromBase64String(cipherText);
            var pdb = new Rfc2898DeriveBytes(_Pwd, _Salt);
            var decryptedData = Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Encoding.Unicode.GetString(decryptedData);
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            var ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                var alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;
                cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
                cs.Write(cipherData, 0, cipherData.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                cs.Close();
            }
        }

        public static string Encrypt(string clearText)
        {
            var clearBytes = Encoding.Unicode.GetBytes(clearText);
            var pdb = new Rfc2898DeriveBytes(_Pwd, _Salt);
            var encryptedData = Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16));
            return Convert.ToBase64String(encryptedData);
        }


        private static byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            var ms = new MemoryStream();
            CryptoStream cs = null;
            try
            {
                var alg = Rijndael.Create();
                alg.Key = Key;
                alg.IV = IV;
                cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
                cs.Write(clearData, 0, clearData.Length);
                cs.FlushFinalBlock();
                return ms.ToArray();
            }
            catch
            {
                return null;
            }
            finally
            {
                cs.Close();
            }
        }
    }
}