using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Reflection;
using System.IO;
using System.Text.Json;
using System.Diagnostics.CodeAnalysis;
using System.Dynamic;
using Atbash.Cryptography;

namespace Atbash.Reflection
{
    public static class ElementsWorker
    {
        public static IEnumerable<ComboBoxItem> GetComboBoxItems()
        {
            var gotClasses = ClassLoader.LoadClasses<ICryptoService<string, string>>();

            var boxList = new List<ComboBoxItem>();

            foreach (var item in gotClasses)
            {
                var boxItem = item?.GetMethod("CreateComboBoxItem")?.Invoke(null, null) as ComboBoxItem;
                if (boxItem != null)
                    boxList.Add(boxItem);
            }

            return boxList;
        }

        public static dynamic? GetSerializedData(string fileName)
        {
            dynamic? data = null;

            using (var deserializeStream = new FileStream($"SerializedData\\{fileName}.json", FileMode.Open))
            {
                data = JsonSerializer.Deserialize(deserializeStream, typeof(ExpandoObject));
            }

            return data;
        }
    }
}
