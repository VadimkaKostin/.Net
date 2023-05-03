using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab4.Interfaces
{
    public interface IDataStorage
    {
        void AddData(string key, string value);
        string? GetData(string key);
    }
}
