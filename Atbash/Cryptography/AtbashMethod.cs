using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using Atbash.LanguageSettings;
using Atbash.Extensions;
using static System.Net.Mime.MediaTypeNames;

namespace Atbash.Cryptography
{
    class AtbashMethod : ICryptoService<string, string>
    {
        private string? _initialText;
        private string? _decryptedText;
        private readonly StringBuilder _stringBuilder;
        private const int _symbolOffset = 1;
        private readonly int _alphabetCount;
        public AtbashMethod(ILanguageSettings<(string lang, int count)> languageSettings) : this()
        {

            LanguageSettings = languageSettings ?? throw new ArgumentNullException(nameof(languageSettings));
            var settings = languageSettings.GetSettings();
            _alphabetCount = settings.count;

        }

        private AtbashMethod()
        {
            _stringBuilder = new StringBuilder();
        }

        public string? GetEncryptedText { get { return _decryptedText; } }

        public string? GetOriginText { get { return _initialText; } }

        private ILanguageSettings<(string lang, int count)>? LanguageSettings { get; set; }

        public string Encrypt(string? data = null)
        {
            if (data == null)
                return string.Empty;

            _initialText = data.ToLower();
            _stringBuilder.Clear();
            _stringBuilder.Append(_initialText);
            return Compute();
        }

        public string Decrypt(string? data = null)
        {
            if (data == null)
                return string.Empty;

            _initialText = data.ToLower();
            _stringBuilder.Clear();
            _stringBuilder.Append(_initialText);
            return Compute();
        }

        private string Compute()
        {
            if (LanguageSettings == null)
                throw new NullReferenceException(nameof(LanguageSettings));

            for (int i = 0; i < _stringBuilder.Length; i++)
            {
                char c = _stringBuilder[i];

                if(char.IsDigit(c) || c.IsServiceSymbol() || c == ' ')
                   continue;

                int code = ((_alphabetCount - LanguageSettings.GetOrderedSymbolNumber(c)) + _symbolOffset);
                _stringBuilder[i] = LanguageSettings.GetSymbol(code);
            }

            _decryptedText = _stringBuilder.ToString();
            return _decryptedText;
        }
    }
}
