﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using System.Windows.Controls;
using Atbash.LanguageSettings;
using Atbash.Extensions;
using System.Windows.Xps.Serialization;

namespace Atbash.Cryptography
{
    public class ROT : ICryptoService<string, string>
    {
        private string? _initialText;
        private string? _decryptedText;
        private readonly StringBuilder _stringBuilder;
        private readonly int _symbolOffset;
        private readonly bool _isRightOffset;

        public ROT(int offset, ILanguageSettings<LanguageParams>? languageSettings, bool rightOffset) : this()
        {
            _isRightOffset = rightOffset;
            _symbolOffset = _isRightOffset ? offset : -offset;
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

            return ROTMethodDecrypt();
        }

        private string ROTMethodDecrypt()
        {
            _decryptedText = "";
            int decryptOffset = _isRightOffset ? -_symbolOffset : Math.Abs(_symbolOffset);

            for (int i = 0; i < _initialText?.Length; i++)
            {
                char letter = _initialText[i];

                if (char.IsDigit(letter) || letter.IsServiceSymbol() || Char.IsWhiteSpace(letter))
                {
                    _stringBuilder.Append(letter);
                    continue;
                }

                int code = LanguageSettings.GetOrderedSymbolNumber(letter) + decryptOffset;

                _stringBuilder.Append(LanguageSettings.GetSymbol(code));
            }

            _decryptedText = _stringBuilder.ToString();

            return _decryptedText;
        }

        private string ROTMethodEncrypt()
        {
            _decryptedText = "";

            for (int i = 0; i < _initialText?.Length; i++)
            {
                char letter = _initialText[i];

                if (char.IsDigit(letter) || letter.IsServiceSymbol() || Char.IsWhiteSpace(letter))
                {
                    _stringBuilder.Append(letter);
                    continue;
                }

                int code = LanguageSettings.GetOrderedSymbolNumber(letter) + _symbolOffset;

                _stringBuilder.Append(LanguageSettings.GetSymbol(code));
            }

            _decryptedText = _stringBuilder.ToString();

            return _decryptedText;
        }

        public string Encrypt(string? data)
        {
            _initialText = data ?? throw new ArgumentNullException(nameof(data));

            return ROTMethodEncrypt();
        }

        public static ComboBoxItem CreateComboBoxItem()
        {
            var boxItem = new ComboBoxItem();

            var margin = new System.Windows.Thickness(5, 5, 5, 5);

            boxItem.Content = new StackPanel
            {
                Orientation = Orientation.Vertical,
                Children =
                {
                    new Label{Content = nameof(ROT) },
                    new StackPanel
                    {
                        Orientation = Orientation.Horizontal,
                        Children =
                        {
                            new StackPanel
                            {
                                Orientation = Orientation.Vertical,
                                Children =
                                {
                                    new Label
                                    {
                                        Content = "Смещение",
                                        Margin = margin
                                    },
                                    new TextBox{Margin = margin}
                                },
                                Margin = margin
                            },
                            new StackPanel
                            {
                                Orientation= Orientation.Horizontal,
                                Children =
                                {
                                    new Label
                                    {
                                        Content = "Правое смещение: ",
                                        Margin = margin
                                    },
                                    new CheckBox{Margin = margin}
                                },
                                Margin = margin
                            },
                        }
                    }
                }
            };

            boxItem.Selected += (sender, e) =>
            {
                var inputData = boxItem.Content as StackPanel;
                var neccessaryData = inputData?.Children[1] as StackPanel;

                var data1 = neccessaryData?.Children[0] as StackPanel;
                var data2 = neccessaryData?.Children[1] as StackPanel;

                var serializeData = new
                {
                    symbolOffset = (data1?.Children[1] as TextBox)?.Text,
                    isRightOffset = (data2?.Children[1] as CheckBox)?.IsChecked
                };

                using (var serializeStream = new FileStream($"SerializedData\\{nameof(ROT)}.json", FileMode.Create))
                {
                    JsonSerializer.Serialize(serializeStream, serializeData);
                }
            };

            return boxItem;
        }
    }
}

