using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atbash.Extensions;

namespace Atbash.LanguageSettings
{
    public  class LatinLanguageSettings : ILanguageSettings<LanguageParams>
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

        public LanguageParams GetSettings()
        {
            return new LanguageParams { Language = _language, SymbolsCount = _symbolsCount };
        }

        public char GetSymbol(int c)
        {
            return Encoding.Unicode.GetSymbol(_firstLetterCode + c);
        }

        public static ILanguageSettings<LanguageParams> CreateSettings()
        {
            return new LatinLanguageSettings();
        }
    }
}
