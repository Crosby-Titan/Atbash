using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.LanguageSettings
{
    internal class CyrillicLanguageSettings : ILanguageSettings<(string lang, int count)>
    {
        private const string _language = "ru";
        private const string _alphabet = "\0абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public (string lang, int count) GetSettings()
        {
            return (lang: _language, count: _alphabet.Length - 1);
        }

        public int GetOrderedSymbolNumber(char value)
        {
            return _alphabet.IndexOf(value);
        }

        public char GetSymbol(int index)
        {
            return _alphabet[index > _alphabet.Length - 1 ? 0 : index];
        }
    }
}
