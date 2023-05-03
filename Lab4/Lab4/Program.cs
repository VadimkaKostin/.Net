using Lab4.Interfaces;
using System;
using System.Security.Cryptography;
using System.Text;

namespace Lab4
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            IDataStorage dataStorage = new DataStorageProxy();
            ITargetCryptoService targetCryptoService = new CryptoServiceAdapter();

            Client client = new Client(dataStorage, targetCryptoService);

            client.RunApplication();
        }
    }
}