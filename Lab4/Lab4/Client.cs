using Lab4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class Client
    {
        private readonly IDataStorage _dataStorage;
        private readonly ITargetCryptoService _cryptoService;

        public Client(IDataStorage dataStorage, ITargetCryptoService cryptoService)
        {
            this._dataStorage = dataStorage;
            this._cryptoService = cryptoService;
        }

        public void RunApplication()
        {
            Console.Write("Enter key: ");
            string? encriptionKey = Console.ReadLine();

            Console.Write("Enter IV: ");
            string? encriptionIV = Console.ReadLine();

            while (true)
            {
                Console.WriteLine("\nMenu");
                Console.WriteLine("1) Encrypt data");
                Console.WriteLine("2) Decrypt data");
                Console.WriteLine("3) Exit");
                Console.Write("Select an option: ");

                string? option = Console.ReadLine();
                switch (option)
                {
                    case "1":
                        Console.Write("Enter data to encrypt: ");
                        string dataToEncrypt = Console.ReadLine();
                        string encryptedText = _cryptoService.EncryptSymmetric(dataToEncrypt, encriptionKey, encriptionIV);
                        string encryptedKey = _cryptoService.Hash(dataToEncrypt);
                        _dataStorage.AddData(encryptedKey, encryptedText);
                        Console.WriteLine("\nData encrypted and stored successfully!");
                        Console.WriteLine($"Key of data in data store: {encryptedKey}");
                        break;

                    case "2":
                        Console.Write("Enter key for decryption: ");
                        string? key = Console.ReadLine();
                        string? encryptedData = _dataStorage.GetData(key);
                        string decryptedText = _cryptoService.DecryptSymmetric(encryptedData, encriptionKey, encriptionIV);
                        Console.WriteLine($"\nDecrypted text: {decryptedText}");
                        break;

                    case "3":
                        return;

                    default:
                        Console.WriteLine("Invalid option selected. Please try again.");
                        break;
                }
            }
        }
    }
}
