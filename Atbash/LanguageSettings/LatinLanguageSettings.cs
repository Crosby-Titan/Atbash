using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atbash.Extensions;

namespace Atbash.LanguageSettings
{
    internal class LatinLanguageSettings : ILanguageSettings<(string lang, int count)>
    {
        private readonly string _language;
        private const int _symbolsCount = 26;
        private readonly int _firstLetterCode;

        public LatinLanguageSettings()
        {
            _language = "en";
            _firstLetterCode = Encoding.Unicode.GetSymbolCode('a') - 1;
        }
        public int GetOrderedSymbolNumber(char value)
        {
            return Encoding.Unicode.GetLatinOrderNumber(value);
        }

        public (string lang, int count) GetSettings()
        {
            return (lang: _language, count: _symbolsCount);
        }

        public char GetSymbol(int c)
        {
            return Encoding.Unicode.GetSymbol(_firstLetterCode + c);
        }
    }
}
