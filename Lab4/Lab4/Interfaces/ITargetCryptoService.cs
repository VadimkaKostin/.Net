using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Interfaces
{
    //Target
    public interface ITargetCryptoService
    {
        public string Hash(string text);
        public string EncryptSymmetric(string text, string keyStr, string ivStr);
        public string DecryptSymmetric(string encryptedText, string keyStr, string ivStr);
        public string EncryptAsymmetric(string text, string publicKeyStr);
        public string DecryptAsymmetric(string encryptedText, string privateKeyStr);
    }
}
