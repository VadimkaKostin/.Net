using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Cryptografy
{
    public class HashService
    {
        public string Hash(string text, HashAlgorithm algorithm)
        {
            byte[] hashBytes = algorithm.ComputeHash(Encoding.UTF8.GetBytes(text));
            return BitConverter.ToString(hashBytes);
        }
    }
}
