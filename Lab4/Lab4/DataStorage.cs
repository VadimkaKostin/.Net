using Lab4.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4
{
    public class DataStorage : IDataStorage
    {
        private readonly Dictionary<string, string> _data;

        public DataStorage()
        {
            _data = new Dictionary<string, string>();
        }

        public void AddData(string key, string value)
        {
            _data[key] = value;
        }

        public string? GetData(string key)
        {
            return _data.ContainsKey(key) ? _data[key] : null;
        }
    }
}
