using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Atbash.Other
{
    internal class SimpleAtbash
    {
        private string? _initialText;//начальный текст
        private string? _decryptedText;//зашифрованный текст
        private const int _symbolOffset = 1;//смещение в шифровании
        private readonly string? _alphabet;//алфавит
        public SimpleAtbash(string lang)//инициализация алфавита в зависимости от языка
        {

            if (lang.ToLower() == "en")
            {
                _alphabet = "\0abcdefghijklmnopqrstuvwxyz";
            }
            if (lang.ToLower() == "ru")
            {
                _alphabet = "\0абвгдеёжзийклмнопрстуфхцчшщъыьэюя";
            }
        }

        public string? GetEncryptedText { get { return _decryptedText; } }//получение зашифрованного текста

        public string? GetOriginText { get { return _initialText; } }//получение оригинального текста

        public string Encrypt(string? data = null)
        {
            if (data == null)
                return string.Empty;

            _initialText = data;
            return AtbashMethod();
        }//шифрование

        private string AtbashMethod()
        {
            _decryptedText = "";

            for (int i = 0; i < _initialText.Length; i++)
            {
                char c = _initialText[i];
                int a = _alphabet.IndexOf(c);
                int d = _alphabet.Length - 1 - a;
                int b = d + _symbolOffset;
                int code = b;
                _decryptedText += _alphabet[code >= _alphabet.Length ? 0 : code];
            }

            return _decryptedText;
        }//метод атбаша

        public string Decrypt(string? data = null)
        {
            if (data == null)
                return string.Empty;

            _initialText = data;
            return AtbashMethod();
        }//Дешифрование
    }
}
