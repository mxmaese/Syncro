using System;
using System.IO;
using System.Security.Cryptography;
using System.Text;

namespace Servicios
{
    public class Encrypt_Decrypt
    {
        public static string GenerateSHA256String(string input)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(input));

                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }

                return builder.ToString();
            }
        }
        private byte[] Encrypt(byte[] clearData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateEncryptor(), CryptoStreamMode.Write);
            cs.Write(clearData, 0, clearData.Length);
            cs.Close();
            byte[] encryptedData = ms.ToArray();
            return encryptedData;
        }

        public string EncryptString(string Data, string Password, int Bits)
        {
            byte[] clearBytes = System.Text.Encoding.Unicode.GetBytes(Data);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x0, 0x1, 0x2, 0x1C, 0x1D, 0x1E, 0x3, 0x4, 0x5, 0xF, 0x20, 0x21, 0xAD, 0xAF, 0xA4 });

            byte[] encryptedData = Bits switch
            {
                128 => Encrypt(clearBytes, pdb.GetBytes(16), pdb.GetBytes(16)),
                192 => Encrypt(clearBytes, pdb.GetBytes(24), pdb.GetBytes(16)),
                256 => Encrypt(clearBytes, pdb.GetBytes(32), pdb.GetBytes(16)),
                _ => throw new ArgumentOutOfRangeException(nameof(Bits), "Invalid encryption bit size specified.")
            };

            return Convert.ToBase64String(encryptedData);
        }

        private static byte[] Decrypt(byte[] cipherData, byte[] Key, byte[] IV)
        {
            MemoryStream ms = new MemoryStream();
            Rijndael alg = Rijndael.Create();
            alg.Key = Key;
            alg.IV = IV;
            CryptoStream cs = new CryptoStream(ms, alg.CreateDecryptor(), CryptoStreamMode.Write);
            cs.Write(cipherData, 0, cipherData.Length);
            cs.Close();
            byte[] decryptedData = ms.ToArray();
            return decryptedData;
        }

        public string DecryptString(string Data, string Password, int Bits)
        {
            byte[] cipherBytes = Convert.FromBase64String(Data);
            PasswordDeriveBytes pdb = new PasswordDeriveBytes(Password, new byte[] { 0x0, 0x1, 0x2, 0x1C, 0x1D, 0x1E, 0x3, 0x4, 0x5, 0xF, 0x20, 0x21, 0xAD, 0xAF, 0xA4 });

            byte[] decryptedData = Bits switch
            {
                128 => Decrypt(cipherBytes, pdb.GetBytes(16), pdb.GetBytes(16)),
                192 => Decrypt(cipherBytes, pdb.GetBytes(24), pdb.GetBytes(16)),
                256 => Decrypt(cipherBytes, pdb.GetBytes(32), pdb.GetBytes(16)),
                _ => throw new ArgumentOutOfRangeException(nameof(Bits), "Invalid decryption bit size specified.")
            };

            return System.Text.Encoding.Unicode.GetString(decryptedData);
        }
    }
}
