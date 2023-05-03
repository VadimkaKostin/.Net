using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptografy
{
    public class AsymmetricEncryptionService
    {
        public byte[] Encrypt(string text, byte[] publicKey)
        {
            byte[] encryptedBytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPublicKey(publicKey, out _);
                encryptedBytes = rsa.Encrypt(Encoding.UTF8.GetBytes(text), true);
            }

            return encryptedBytes;
        }

        public byte[] Decrypt(string encryptedText, byte[] privateKey)
        {
            byte[] decryptedBytes;

            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.ImportRSAPrivateKey(privateKey, out _);
                decryptedBytes = rsa.Decrypt(Convert.FromBase64String(encryptedText), true);
            }

            return decryptedBytes;
        }
    }
}
