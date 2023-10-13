using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Atbash.LanguageSettings;

namespace Atbash.Cryptography
{
    class ROT : ICryptoService<string, string>
    {
        private string? _initialText;
        private string? _decryptedText;
        private readonly StringBuilder _stringBuilder;
        private readonly int _symbolOffset;

        public ROT(int offset, ILanguageSettings<LanguageParams>? languageSettings,bool rightOffset): this()
        {
            _symbolOffset = rightOffset ? offset : -offset;
            LanguageSettings = languageSettings ?? throw new ArgumentNullException(nameof(languageSettings));
        }

        public ROT(ILanguageSettings<LanguageParams>? languageSettings, bool rightOffset) : this()
        {
            LanguageSettings = languageSettings ?? throw new ArgumentNullException(nameof(languageSettings));
            _symbolOffset = 13;
            _symbolOffset = rightOffset ? _symbolOffset : -_symbolOffset;
        }

        public ILanguageSettings<LanguageParams> LanguageSettings { get; private set; }

        private ROT()
        {
            _stringBuilder = new StringBuilder();
        }

        public string Decrypt(string? data)
        {

            _initialText = data ?? throw new ArgumentNullException(nameof(data));

            return ROTMethod();
        }

        private string ROTMethod()
        {
            _decryptedText = "";

            for(int i = 0; i < _initialText.Length;i++)
            {
                char letter = _initialText[i];

                int code = LanguageSettings.GetOrderedSymbolNumber(letter) + _symbolOffset;

                _stringBuilder.Append(LanguageSettings.GetSymbol(code));
            }

            _decryptedText = _stringBuilder.ToString();

            return _decryptedText;
        }

        public string Encrypt(string? data)
        {
            _initialText = data ?? throw new ArgumentNullException(nameof(data));

            return ROTMethod();
        }
    }
}
