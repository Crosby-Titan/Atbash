using Atbash.Cryptography;
using Atbash.LanguageSettings;
using Atbash.Reflection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Atbash
{
    public partial class MainWindow
    {
        private IEnumerable<ComboBoxItem> _cryptographyMethod;
        private IDictionary<string, Func<ILanguageSettings<LanguageParams>>> _symbolsType;
        private ICryptoService<string, string>? _cryptography = null;
        private void OffsetCryptography_InitializeMethod(ILanguageSettings<LanguageParams> settings, string? cryptoMethod)
        {
            switch (cryptoMethod)
            {
                case nameof(AtbashMethod):
                    _cryptography = new AtbashMethod(settings);
                    break;
                case nameof(ROT):
                    var data = ElementsWorker.GetSerializedData($"{nameof(ROT)}");
                    var offset = ElementsWorker.ParseData(data?.symbolOffset);
                    var rightOffset = ElementsWorker.ParseData(data?.isRightOffset);

                    if (!ElementsWorker.ValidateOffset(settings.GetSettings().SymbolsCount, offset))
                    {
                        MessageBox.Show("Значение смещения не соответствует допустимому диапазону.",
                            "Ошибка",
                            MessageBoxButton.OK,
                            MessageBoxImage.Error);
                        return;
                    }

                    _cryptography = new ROT(offset, settings, rightOffset);

                    break;
                default:
                    _cryptography = new AtbashMethod(settings);
                    break;
            }
        }

        private void OffsetCryptography_Process()
        {
            OffsetCryptography_GetInput();

            if (DecryptionMethod.IsChecked == true)
            {
                FinalText_OffsetCryptography.Text =
                    $"{_cryptography?.Decrypt(InitialText_OffsetCryptography.Text.Trim().ToLower())}";
            }
            else if (EncryptionMethod.IsChecked == true)
            {
                FinalText_OffsetCryptography.Text =
                    $"{_cryptography?.Encrypt(InitialText_OffsetCryptography.Text.Trim().ToLower())}";
            }
            else
            {
                MessageBox.Show("Не выбран метод!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool OffsetCryptography_CheckInputText()
        {
            if (InitialText_OffsetCryptography.Text.Length < 1)
            {
                MessageBox.Show("Не введён текст!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool OffsetCryptography_CheckSelectedMethod()
        {
            return this.CryptoMethodList.SelectedIndex > -1;
        }

        private bool OffsetCryptography_CheckSelectedAlphabet()
        {
            return this.LanguageList.SelectedIndex > -1;
        }

        private void OffsetCryptography_GetInput()
        {

            var cryptoMethod = (CryptoMethodList.SelectedValue as ComboBoxItem)?.Content as StackPanel;
            string? language = LanguageList.SelectedValue.ToString();

            if (cryptoMethod == null || language == null)
                throw new ArgumentNullException(nameof(cryptoMethod));

            OffsetCryptography_InitializeMethod(_symbolsType[language].Invoke(), (cryptoMethod.Children[0] as Label)?.Content?.ToString());
        }

        private void OffsetCryptography_TemplateMethod()
        {
            bool validUserInput = OffsetCryptography_CheckSelectedMethod()
                && OffsetCryptography_CheckInputText()
                && OffsetCryptography_CheckSelectedAlphabet();

            if (validUserInput)
            {
                OffsetCryptography_Process();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void OffsetCryptography_LoadResource()
        {
            _cryptographyMethod = ElementsWorker.GetComboBoxItems();
            _symbolsType = new Dictionary<string, Func<ILanguageSettings<LanguageParams>>>
            {
                { "Латиница", LatinLanguageSettings.CreateSettings },
                { "Кириллица", CyrillicLanguageSettings.CreateSettings }
            };

            LanguageList.ItemsSource = _symbolsType.Keys;
            CryptoMethodList.ItemsSource = _cryptographyMethod;
        }

    }
}
