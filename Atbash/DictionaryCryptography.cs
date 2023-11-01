using Atbash.Cryptography;
using Atbash.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using Atbash.Extensions;
using System.Windows.Input;
using System.Windows.Controls;

namespace Atbash
{
    public partial class MainWindow
    {
        private string[] _cryptoMethodsName = { nameof(MorseCode),nameof(BinaryCode) };
        private string[] _langList = { "Кириллица", "Латиница" };

        private void DictionaryCryptography_TemplateMethod()
        {
            bool validUserInput = DictionaryCryptography_CheckInput()
                && DictionaryCryptography_CheckSelectedMethod()
                && DictionaryCryptography_CheckSelectedAlphabet();

            if (validUserInput)
            {
                DictionaryCryptography_Process();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
        private void DictionaryCryptography_InitializeMethod()
        {
            switch (CryptoMethodList_DictionaryCryptography.SelectedItem.ToString())
            {
                case nameof(MorseCode):
                    _cryptography = new MorseCode();
                    break;
                case nameof(BinaryCode):
                    int codepage = GetCodePage(LanguageList_DictionaryCryptography.SelectedItem.ToString());
                    _cryptography = new BinaryCode(Encoding.GetEncoding(codepage));
                    break;
                default:
                    break;
            }
        }
        private bool DictionaryCryptography_CheckInput()
        {
            return InitialText_DictionaryCryptography.Text.Length > 0;
        }

        private bool DictionaryCryptography_CheckSelectedMethod()
        {
            return CryptoMethodList_DictionaryCryptography.SelectedIndex > -1;
        }

        private bool DictionaryCryptography_CheckSelectedAlphabet()
        {
            return LanguageList_DictionaryCryptography.SelectedIndex > -1;
        }

        private void DictionaryCryptography_LoadResource()
        {
            LanguageList_DictionaryCryptography.ItemsSource = _langList;

            CryptoMethodList_DictionaryCryptography.ItemsSource = _cryptoMethodsName;

            EncryptionMethod_DictionaryCryptography.IsChecked = false;
            DecryptionMethod_DictionaryCryptography.IsChecked = false;

            EncryptionMethod_DictionaryCryptography.Checked += OnSelectedNewMethod;
            DecryptionMethod_DictionaryCryptography.Checked += OnSelectedNewMethod;
            CryptoMethodList_DictionaryCryptography.SelectionChanged += MethodChanged;
        }

        private void MethodChanged(object sender, SelectionChangedEventArgs e)
        {
            if(CryptoMethodList_DictionaryCryptography.SelectedItem.ToString() == nameof(MorseCode))
            {
                DecryptionMethod_DictionaryCryptography.Unchecked += OnUnselectMethod;
                DecryptionMethod_DictionaryCryptography.Checked += MorseCodeOnlyEnable;
            }
            else
            {
                DecryptionMethod_DictionaryCryptography.Unchecked -= OnUnselectMethod;
                DecryptionMethod_DictionaryCryptography.Checked -= MorseCodeOnlyEnable;
            }
        }

        private void OnUnselectMethod(object sender, RoutedEventArgs e)
        {
            InitialText_DictionaryCryptography.TextChanged -= MorseOnlyHandler;
        }

        private void DictionaryCryptography_Process()
        {
            DictionaryCryptography_InitializeMethod();

            if (DecryptionMethod_DictionaryCryptography.IsChecked == true)
            {
                FinalText_DictionaryCryptography.Text =
                    $"{_cryptography?.Decrypt(InitialText_DictionaryCryptography.Text.Trim().ToLower())}";
            }
            else if (EncryptionMethod_DictionaryCryptography.IsChecked == true)
            {
                FinalText_DictionaryCryptography.Text =
                    $"{_cryptography?.Encrypt(InitialText_DictionaryCryptography.Text.Trim().ToLower())}";
            }
            else
            {
                MessageBox.Show("Не выбран метод!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        public void OnSelectedNewMethod(object sender, EventArgs e)
        {
            InitialText_DictionaryCryptography.Text = "";
        }

        private void MorseCodeOnlyEnable(object sender, RoutedEventArgs e)
        {

            InitialText_DictionaryCryptography.TextChanged += MorseOnlyHandler;

        }

        private void MorseOnlyHandler(object sender, TextChangedEventArgs e)
        {
            if (InitialText_DictionaryCryptography.Text.Length < 1)
                return;

            string morseSymbols = ".-";

            StringBuilder stringBuilder = new StringBuilder(InitialText_DictionaryCryptography.Text);

            for (int i = 0; i < stringBuilder.Length; i++)
            {
                if (stringBuilder[i].IsServiceSymbol() || Char.IsWhiteSpace(stringBuilder[i]))
                    continue;


                if (!morseSymbols.Contains(stringBuilder[i]))
                {
                    int index = stringBuilder.IndexOf(stringBuilder[i]);

                    stringBuilder.Remove(index, 1);
                }
            }

            InitialText_DictionaryCryptography.Text = stringBuilder.ToString();

        }

        private int GetCodePage(string lang)
        {
            switch(lang)
            {
                case "Кириллица":
                    return 1251;
                case "Латиница":
                    return Encoding.ASCII.CodePage;
                default:
                    return Encoding.ASCII.CodePage;
            }
        }
    }
}
