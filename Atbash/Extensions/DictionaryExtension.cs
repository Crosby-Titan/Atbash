using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Extensions
{
    public static class DictionaryExtension
    {
        public static dynamic? GetKeyByValue<T,V>(this IDictionary<T, V> dictionary, dynamic value)
        {
            foreach (var kvp in dictionary)
            {
                if (kvp.Value == value)
                    return kvp.Key;
            }

            return null;
        }
    }
}
