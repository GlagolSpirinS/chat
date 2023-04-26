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

namespace full_chat
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        public static string nameTexBox = "";

        private void open_server_menu(object sender, RoutedEventArgs e)
        {
            serverMenu serverMenu = new serverMenu();

            serverMenu.Show();

            this.Close();
        }
    
    private void open_user_menu(object sender, RoutedEventArgs e)
    {
        try
        {
            nameTexBox = name.Text;

            string ipText = ip.Text;

            userMenu userMenu = new userMenu(ipText);

            userMenu.Show();

            this.Close();
        }
        catch(Exception ex) 
        { 
            MessageBox.Show("Вам нужно ввести IP адресс для подключения", "БЛЯТЬ ТЫ ТУПОЙ ИЛИ ДА!?", MessageBoxButton.OK);
        }
    }

        private void ip_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox ipTextbox = (TextBox)sender;

            for (int i = 0; i < ipTextbox.Text.Length; i++)
            {
                char c = ipTextbox.Text[i];

                if (!Char.IsDigit(c) && c != '.')
                {
                    ipTextbox.Text = ipTextbox.Text.Remove(i, 1);
                    i--;
                }
            }
        }

        private void jopa_TextChanged(object sender, TextChangedEventArgs e)
        {
            TextBox nameTextBox = (TextBox)sender;
        }
    }
}
