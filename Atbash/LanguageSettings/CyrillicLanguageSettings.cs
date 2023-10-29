using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.LanguageSettings
{
    public class CyrillicLanguageSettings : ILanguageSettings<LanguageParams>
    {
        private const string _language = "ru";
        private const string _alphabet = "\0абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
        public LanguageParams GetSettings()
        {
            return new LanguageParams { Language = _language, SymbolsCount = _alphabet.Length };
        }

        public int GetOrderedSymbolNumber(char value)
        {
            return _alphabet.IndexOf(value);
        }

        public char GetSymbol(int index)
        {
            if (index < 0)
                return _alphabet[(_alphabet.Length - 1) + index];
            else if (index > (_alphabet.Length - 1))
                return _alphabet[Math.Abs((_alphabet.Length - 1) - index)];
            else if (index == 0)
            {
                return _alphabet[1];
            }
            else
                return _alphabet[index];
        }

        public static ILanguageSettings<LanguageParams> CreateSettings()
        {
            return new CyrillicLanguageSettings();
        }

    }
}
