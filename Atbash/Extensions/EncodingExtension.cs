using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Extensions
{
    internal static class EncodingExtension
    {
        public static int GetSymbolCode(this Encoding encoding, char c)
        {
            return c;
        }

        public static char GetSymbol(this Encoding encoding, int code)
        {
            return (char)code;
        }

        public static int GetLatinOrderNumber(this Encoding encoding, char c)
        {
            int code = Math.Abs(encoding.GetSymbolCode('a') - encoding.GetSymbolCode(c));

            if (code < 0)
                return encoding.GetSymbolCode('z') + code;
            else if (code > encoding.GetSymbolCode('z'))
                return encoding.GetSymbolCode('a') + (encoding.GetSymbolCode('z') - code);
            else
                return code;
        }
    }
}
