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

            using (var deserializeStream = new FileStream($"JsonData\\{fileName}.json", FileMode.Open))
            {
                data = JsonSerializer.Deserialize(deserializeStream, typeof(ExpandoObject));
            }

            return data;
        }

        public static dynamic ParseData(dynamic data)
        {
            switch (data.ValueKind)
            {
                case JsonValueKind.String:
                    {
                        bool isDigit = false;
                        foreach (var ch in $"{data}")
                        {
                            if (Char.IsDigit(ch))
                                isDigit = true;
                            else
                                isDigit = false;
                        }

                        if (isDigit)
                            goto case JsonValueKind.Number;
                        else
                            return $"{data}";
                    }
                case JsonValueKind.Number:
                    return int.Parse($"{data}");
                case JsonValueKind.True:
                case JsonValueKind.False:
                    return bool.Parse($"{data}");
                case JsonValueKind.Object:
                    return LoadMorseCode($"{data}");
                default:
                    return string.Empty;
            }
        }

        public static IDictionary<string, string> LoadMorseCode(string str)
        {
            var dictionary = new Dictionary<string, string>();

            for (int i = 0; i < str.Length; i++)
            {
                if (dictionary.ContainsKey(str[i].ToString()) || str[i] == ' ')
                    continue;

                string key = str[i].ToString();

                if (key == "X")
                    break;

                string value = "";

                for (int j = i + 2; j < str.Length; j++)
                {
                    if (str[j] == ',')
                    {
                        i = j + 1;
                        break;
                    }

                    if (str[j] != ' ')
                        value += str[j];
                }

                dictionary.Add(key, value);


            }

            return dictionary;
        }

        public static bool ValidateOffset(int alphabetCount,int offset)
        {
            if (offset < 0 || offset > alphabetCount)
            {
                return false;
            }

            return true;
        }
    }
}
