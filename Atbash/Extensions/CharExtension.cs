using System;
using System.Collections.Generic;
using System.Diagnostics.SymbolStore;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Extensions
{
    public static class CharExtension
    {
        private static string _serviceSymbol = "!@#$%^&*()_+-={}|;:>.?<,\\/[]'\"";
        public static bool IsServiceSymbol(this char Base,string? exceptOf = null)
        {
            if(exceptOf?.Contains(Base) == true) return false;

            if(_serviceSymbol.Contains(Base))return true;

            return false;
        }
    }
}
