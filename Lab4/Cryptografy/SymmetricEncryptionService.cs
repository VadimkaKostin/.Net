using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptografy
{
    public class SymmetricEncryptionService
    {
        public byte[] Encrypt(string text, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
        {
            byte[] encryptedBytes;

            using (ICryptoTransform encryptor = algorithm.CreateEncryptor(key, rgbIV: iv))
            {
                byte[] plainBytes = Encoding.UTF8.GetBytes(text);
                encryptedBytes = encryptor.TransformFinalBlock(plainBytes, 0, plainBytes.Length);
            }

            return encryptedBytes;
        }

        public byte[] Decrypt(string encryptedText, SymmetricAlgorithm algorithm, byte[] key, byte[] iv)
        {
            byte[] decryptedBytes;

            using (ICryptoTransform decryptor = algorithm.CreateDecryptor(key, rgbIV: iv))
            {
                byte[] encryptedBytes = Convert.FromBase64String(encryptedText);
                decryptedBytes = decryptor.TransformFinalBlock(encryptedBytes, 0, encryptedBytes.Length);
            }

            return decryptedBytes;
        }

    }
}
