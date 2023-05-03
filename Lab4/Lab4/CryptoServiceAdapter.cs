using Cryptografy;
using Lab4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    //Adapter
    public class CryptoServiceAdapter : ITargetCryptoService
    {
        private readonly ICryptoService _cryptoService;

        public CryptoServiceAdapter()
        {
            _cryptoService = new CryptoService();
        }

        public string Hash(string text)
        {
            return _cryptoService.Hash(text);
        }

        public string EncryptSymmetric(string text, string keyStr, string ivStr)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyStr);
            byte[] iv = Encoding.UTF8.GetBytes(ivStr);

            byte[] encriptedData = _cryptoService.EncryptSymmetric(text, key, iv);

            return Convert.ToBase64String(encriptedData);
        }

        public string DecryptSymmetric(string encryptedText, string keyStr, string ivStr)
        {
            byte[] key = Encoding.UTF8.GetBytes(keyStr);
            byte[] iv = Encoding.UTF8.GetBytes(ivStr);

            byte[] decriptedData = _cryptoService.DecryptSymmetric(encryptedText, key, iv);

            return Encoding.UTF8.GetString(decriptedData);
        }

        public string EncryptAsymmetric(string text, string publicKeyStr)
        {
            byte[] publicKey = Encoding.UTF8.GetBytes(publicKeyStr);
            byte[] encriptedData = _cryptoService.EncryptAsymmetric(text, publicKey);
            return Convert.ToBase64String(encriptedData);
        }

        public string DecryptAsymmetric(string encryptedText, string privateKeyStr)
        {
            byte[] privateKey = Encoding.UTF8.GetBytes(privateKeyStr);
            byte[] decriptedData = _cryptoService.DecryptAsymmetric(encryptedText, privateKey);
            return Encoding.UTF8.GetString(decriptedData);
        }
    }
}
