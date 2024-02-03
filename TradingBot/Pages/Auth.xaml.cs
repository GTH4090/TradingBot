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

namespace TradingBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для Auth.xaml
    /// </summary>
    public partial class Auth : Page
    {
        public Auth()
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

        private void LoginBtn_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                if(Db.Clients.FirstOrDefault(el => el.Login == LoginTbx.Text && el.Password == PasswordPbx.Password) != null)
                {
                    logined = Db.Clients.FirstOrDefault(el => el.Login == LoginTbx.Text && el.Password == PasswordPbx.Password);
                    NavigationService.Navigate(new TradingMenu());
                }
                else
                {
                    Info("Неправильынй логин или пароль");
                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
    }
}
