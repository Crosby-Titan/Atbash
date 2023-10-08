using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Atbash.LanguageSettings
{
    internal interface ILanguageSettings<T>
    {
        T GetSettings();
        int GetOrderedSymbolNumber(char c);
        char GetSymbol(int c);
    }
}
