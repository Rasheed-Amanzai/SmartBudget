using System.Security.Cryptography;
using System.Text;

namespace SmartBudget.Services
{
    public class EncryptionService
    {
        private readonly string key = "SmartBudget-AES"; // This should be a securely stored key

        public string Encrypt(string data)
        {
            using (var aesAlg = Aes.Create())
            {
                // Ensure the key length is 32 bytes (256 bits)
                aesAlg.Key = GetValidKey();
                aesAlg.IV = new byte[16]; // Initialization vector set to zero for simplicity

                ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
                byte[] encrypted = PerformCryptography(Encoding.UTF8.GetBytes(data), encryptor);

                return Convert.ToBase64String(encrypted);
            }
        }

        public string Decrypt(string encryptedData)
        {
            using (var aesAlg = Aes.Create())
            {
                // Ensure the key length is 32 bytes (256 bits)
                aesAlg.Key = GetValidKey();
                aesAlg.IV = new byte[16]; // Initialization vector set to zero for simplicity

                ICryptoTransform decryptor = aesAlg.CreateDecryptor(aesAlg.Key, aesAlg.IV);
                byte[] decrypted = PerformCryptography(Convert.FromBase64String(encryptedData), decryptor);

                return Encoding.UTF8.GetString(decrypted);
            }
        }

        private byte[] PerformCryptography(byte[] data, ICryptoTransform transform)
        {
            using (var memoryStream = new System.IO.MemoryStream())
            {
                using (var cryptoStream = new CryptoStream(memoryStream, transform, CryptoStreamMode.Write))
                {
                    cryptoStream.Write(data, 0, data.Length);
                    cryptoStream.FlushFinalBlock();
                }

                return memoryStream.ToArray();
            }
        }

        private byte[] GetValidKey()
        {
            // Ensure the key is 32 bytes long (256 bits), pad if necessary
            byte[] keyBytes = Encoding.UTF8.GetBytes(key);
            if (keyBytes.Length < 32)
            {
                Array.Resize(ref keyBytes, 32);
            }
            else if (keyBytes.Length > 32)
            {
                Array.Resize(ref keyBytes, 32); // Optionally truncate to 32 bytes
            }

            return keyBytes;
        }
    }

}
