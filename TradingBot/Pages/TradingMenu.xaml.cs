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
using YahooFinanceApi;


namespace TradingBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для TradingMenu.xaml
    /// </summary>
    public partial class TradingMenu : Page
    {
        public TradingMenu()
        {
            InitializeComponent();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                var history = await Yahoo.GetHistoricalAsync("IBM", new DateTime(2016, 1, 1), new DateTime(2016, 7, 1), Period.Daily);
                ItemsDg.ItemsSource = history.ToList();
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
    }
}
