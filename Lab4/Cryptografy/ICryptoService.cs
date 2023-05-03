using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptografy
{
    //Adaptee
    public interface ICryptoService
    {
        public string Hash(string text);
        public byte[] EncryptSymmetric(string text, byte[] key, byte[] iv);
        public byte[] DecryptSymmetric(string encryptedText, byte[] key, byte[] iv);
        public byte[] EncryptAsymmetric(string text, byte[] publicKey);
        public byte[] DecryptAsymmetric(string encryptedText, byte[] privateKey);
    }
}
