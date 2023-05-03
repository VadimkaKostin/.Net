using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptografy
{
    public class CryptoService : ICryptoService
    {
        private readonly HashService _hashService;
        private readonly SymmetricEncryptionService _symmetricEncryptionService;
        private readonly AsymmetricEncryptionService _asymmetricEncryptionService;
        private readonly SymmetricAlgorithm _symmetricAlgorithm;
        private readonly HashAlgorithm _hashAlgorithm;
        public CryptoService()
        {
            _hashService = new HashService();
            _symmetricEncryptionService = new SymmetricEncryptionService();
            _asymmetricEncryptionService = new AsymmetricEncryptionService();
            _symmetricAlgorithm = new AesCryptoServiceProvider();
            _hashAlgorithm = new HMACMD5();
        }
        public string Hash(string text)
        {
            return _hashService.Hash(text, _hashAlgorithm);
        }
        public byte[] EncryptSymmetric(string text, byte[] key, byte[] iv)
        {
            return _symmetricEncryptionService.Encrypt(text, _symmetricAlgorithm, key, iv);
        }
        public byte[] DecryptSymmetric(string encryptedText, byte[] key, byte[] iv)
        {
            return _symmetricEncryptionService.Decrypt(encryptedText, _symmetricAlgorithm, key, iv);
        }
        public byte[] EncryptAsymmetric(string text, byte[] publicKey)
        {
            return _asymmetricEncryptionService.Encrypt(text, publicKey);
        }
        public byte[] DecryptAsymmetric(string encryptedText, byte[] privateKey)
        {
            return _asymmetricEncryptionService.Decrypt(encryptedText, privateKey);
        }
    }
}
