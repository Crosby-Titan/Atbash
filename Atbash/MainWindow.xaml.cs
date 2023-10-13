using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Atbash.Cryptography;
using Atbash.LanguageSettings;

namespace Atbash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //private string[] _symbolsType = new[] { "Латиница", "Кириллица" };
        private IDictionary<string, string> _cryptographyMethod;
        private IDictionary<string, Func<ILanguageSettings<LanguageParams>>> _symbolsType;
        private ICryptoService<string, string>? _cryptography = null;
        public MainWindow()
        {
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;

            _cryptographyMethod = new Dictionary<string, string>
            {
                {"Метод атбаша", nameof(AtbashMethod)},
                {"Шифр цезаря",nameof(ROT) }
            };

            _symbolsType = new Dictionary<string, Func<ILanguageSettings<LanguageParams>>>
            {
                { "Латиница", LatinLanguageSettings.CreateSettings },
                { "Кириллица", CyrillicLanguageSettings.CreateSettings }
            };

            LanguageList.ItemsSource = _symbolsType.Keys;
            CryptoMethodList.ItemsSource = _cryptographyMethod.Keys;
        }

        private void ComputeBtn_Click(object sender, RoutedEventArgs e)
        {
            bool validUserInput = CheckSelectedMethod()
                && CheckInputText()
                && CheckSelectedAlphabet();

            if (validUserInput)
            {
                Process();
            }
            else
            {
                MessageBox.Show("Что-то пошло не так.", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void InitializeMethod(ILanguageSettings<LanguageParams> settings, string cryptoMethod)
        {
            switch (cryptoMethod)
            {
                case nameof(AtbashMethod):
                    _cryptography = new AtbashMethod(settings);
                    break;
                case nameof(ROT):
                    _cryptography = new ROT(settings, true);
                    break;
                default:
                    _cryptography = new AtbashMethod(settings);
                    break;
            }
        }


        private void Process()
        {
            GetInput();

            if (DecryptionMethod.IsChecked == true)
            {
                FinalText.Text = $"{_cryptography?.Decrypt(InitialText.Text.Trim())}";
            }
            else if (EncryptionMethod.IsChecked == true)
            {
                FinalText.Text = $"{_cryptography?.Encrypt(InitialText.Text.Trim())}";
            }
            else
            {
                MessageBox.Show("Не выбран метод!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool CheckInputText()
        {
            if (this.InitialText.Text == null || this.InitialText.Text.Length < 1)
            {
                MessageBox.Show("Не введён текст!",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckSelectedMethod()
        {
            return this.CryptoMethodList.SelectedIndex == -1;
        }

        private bool CheckSelectedAlphabet()
        {
            return this.LanguageList.SelectedIndex == -1;
        }

        private void GetInput()
        {

            string? cryptoMethod = CryptoMethodList.SelectedValue.ToString();
            string? language = LanguageList.SelectedValue.ToString();

            if (cryptoMethod == null || language == null)
                throw new ArgumentNullException(nameof(cryptoMethod));

            InitializeMethod(_symbolsType[language].Invoke(), _cryptographyMethod[cryptoMethod]);
        }
    }
}
