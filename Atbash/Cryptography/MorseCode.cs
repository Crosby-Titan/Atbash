using Atbash.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Atbash.Extensions;
using System.Threading.Tasks;

namespace Atbash.Cryptography
{
    class MorseCode : ICryptoService<string, string>
    {
        private IDictionary<string, string> _morseTable;
        private StringBuilder _stringBuilder;

        public MorseCode()
        {
            string s = $"{ElementsWorker.GetSerializedData("code")?.MorseCodeCyrillic}".Replace("\r\n", "")
                .Replace("\"", "").Replace("{", "").Replace("}", "").Trim();
            _morseTable = ElementsWorker.LoadMorseCode(s);
            _stringBuilder = new StringBuilder();
        }

        public string Decrypt(string? data)
        {
            if(data == null)
                throw new ArgumentNullException(nameof(data));

            return MorseDecrypt(data);
        }

        public string Encrypt(string? data)
        {
            if (data == null)
                throw new ArgumentNullException(nameof(data));

            return MorseEncrypt(data);
        }

        private string MorseEncrypt(string data)
        {
            _stringBuilder.Clear();

            foreach(var ch in data)
            {
                if (ch.IsServiceSymbol() || Char.IsWhiteSpace(ch))
                {
                    _stringBuilder.Append(ch);
                    continue;
                }

                if (!_morseTable.Keys.Contains(Char.ToUpper(ch).ToString()))
                {
                    _stringBuilder.Append(ch);
                    continue;
                }

                _stringBuilder.Append(_morseTable[Char.ToUpper(ch).ToString()] + "  ");
            }

            return _stringBuilder.ToString();
        }

        private string MorseDecrypt(string data)
        {
            _stringBuilder.Clear();

            string[] input = data.Split("  ");

            for (int i = 0;i < input.Length;i++)
            {
                string code = "";
                for(int j = 0;j < input[i].Length;j++)
                {
                    if (input[i][j] == '.' || input[i][j] == '-')
                    {
                        code += input[i][j];
                    }

                    if (input[i][j].IsServiceSymbol(".-") || Char.IsWhiteSpace(input[i][j]))
                    {
                        _stringBuilder.Append(input[i][j]);
                        continue;
                    }
                }
                _stringBuilder.Append($"{_morseTable.GetKeyByValue(code)}");
            }

            return _stringBuilder.ToString();
        }
    }
}
