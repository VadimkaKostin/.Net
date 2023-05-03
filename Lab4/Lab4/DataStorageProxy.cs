using Lab4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class DataStorageProxy : IDataStorage
    {
        private readonly IDataStorage _dataStorage;

        public DataStorageProxy()
        {
            _dataStorage = new DataStorage();
        }

        public void AddData(string key, string value)
        {
            if(_dataStorage.GetData(key) != null)
            {
                throw new ArgumentException("The record with this key is already present in the data store.");
            }

            _dataStorage.AddData(key, value);
        }

        public string? GetData(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentException("Key is required");
            }

            return _dataStorage.GetData(key);
        }
    }
}
