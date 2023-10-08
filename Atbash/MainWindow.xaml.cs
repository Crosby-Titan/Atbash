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
        private string[] _symbolsType = { "Латиница", "Кириллица" };
        private ICryptoService<string, string>? _atbashCryptography = null;
        public MainWindow()
        {
            InitializeComponent();
            this.Height = SystemParameters.PrimaryScreenHeight;
            this.Width = SystemParameters.PrimaryScreenWidth;
            this.LanguageList.ItemsSource = _symbolsType;
        }

        private void ComputeBtn_Click(object sender, RoutedEventArgs e)
        {
            if (!CheckInputText()) return;

            CheckSettings();

            CheckMethod();
        }

        private void InitializeMethod(ILanguageSettings<(string lang,int count)> settings)
        {
            this._atbashCryptography = new AtbashMethod(settings);
        }

        private void CheckSettings()
        {
            switch (this.LanguageList.SelectedIndex)
            {
                case 0:
                    InitializeMethod(new LatinLanguageSettings());
                    break;
                case 1:
                    InitializeMethod(new CyrillicLanguageSettings());
                    break;
                default:
                    MessageBox.Show("Не выбран алфавит!", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    break;
            }
        }

        private void CheckMethod()
        {
            if (this.DecryptionMethod.IsChecked == true)
            {
                this.FinalText.Text = $"{_atbashCryptography?.Decrypt(this.InitialText.Text.Trim())}";
            }
            else if (this.EncryptionMethod.IsChecked == true)
            {
                this.FinalText.Text = $"{_atbashCryptography?.Encrypt(this.InitialText.Text.Trim())}";
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
            if(this.InitialText.Text == null || this.InitialText.Text.Length < 1)
            {
                MessageBox.Show("Не введён текст!",
                    "Ошибка",
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
                return false;
            }

            return true;
        }
    }
}
