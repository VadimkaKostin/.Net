using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1.ExtantionMethods
{
    public static class IEnumerableExt
    {
        public static void Foreach<T>(this IEnumerable<T> collection, Action<T> action)
        {
            foreach(var item in collection)
            {
                action(item);
            }
        }
    }
}
