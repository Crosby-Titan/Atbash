using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Extensions
{
    public static class CharExtension
    {
        private static string _serviceSymbol = "!@#$%^&*()_+-={}|;:>.?<,\\/[]'\"";
        public static bool IsServiceSymbol(this char Base)
        {

            if(_serviceSymbol.Contains(Base))return true;

            return false;
        }
    }
}
