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
using static TradingBot.Classes.Helper;
using TradingBot.Models;

namespace TradingBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для Registration.xaml
    /// </summary>
    public partial class Registration : Page
    {
        public Registration()
        {
            InitializeComponent();
        }
        private void PasswordPbx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (PasswordPbx.Password != PasswordTbx.Text)
            {
                PasswordTbx.Text = PasswordPbx.Password;
            }
        }

        private void PasswordTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (PasswordPbx.Password != PasswordTbx.Text)
            {
                PasswordPbx.Password = PasswordTbx.Text;
            }
        }

        private void PasswordBtn_Click(object sender, RoutedEventArgs e)
        {
            if (PasswordTbx.Visibility == Visibility.Collapsed)
            {
                PasswordTbx.Visibility = Visibility.Visible;
                PasswordPbx.Visibility = Visibility.Collapsed;
            }
            else
            {
                PasswordPbx.Visibility = Visibility.Visible;
                PasswordTbx.Visibility = Visibility.Collapsed;
            }

        }

        private void RegBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(Password2Pbx.Password == PasswordPbx.Password)
                {
                    Client client = new Client();
                    client.Login = LoginTbx.Text;
                    client.Password = PasswordPbx.Password;
                    Db.Clients.Add(client);
                    Db.SaveChanges();
                    NavigationService.GoBack();
                }
               
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private void Password2Tbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (Password2Pbx.Password != Password2Tbx.Text)
            {
                Password2Pbx.Password = Password2Tbx.Text;
            }
        }

        private void Password2Pbx_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (Password2Pbx.Password != Password2Tbx.Text)
            {
                Password2Tbx.Text = Password2Pbx.Password;
            }
        }

        private void Password2Btn_Click(object sender, RoutedEventArgs e)
        {
            if (Password2Tbx.Visibility == Visibility.Collapsed)
            {
                Password2Tbx.Visibility = Visibility.Visible;
                Password2Pbx.Visibility = Visibility.Collapsed;
            }
            else
            {
                Password2Pbx.Visibility = Visibility.Visible;
                Password2Tbx.Visibility = Visibility.Collapsed;
            }
        }
    }
}
