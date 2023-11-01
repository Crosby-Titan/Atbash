using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
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
using Atbash.Reflection;

namespace Atbash
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Height = SystemParameters.PrimaryScreenHeight;
            Width = SystemParameters.PrimaryScreenWidth;

            DictionaryCryptography_LoadResource();
            OffsetCryptography_LoadResource();
        }

        private void ComputeBtn_Click(object sender, RoutedEventArgs e)
        {
            switch(PagesControl.SelectedIndex)
            {
                case 0:
                    OffsetCryptography_TemplateMethod();
                    break;
                case 1:
                    DictionaryCryptography_TemplateMethod();
                    break;
                default:
                    break;
            }
        }

        

    }
}
