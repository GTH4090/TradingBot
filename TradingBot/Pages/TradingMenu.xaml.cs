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
using TradingBot.Models;
using LiveChartsCore;
using LiveChartsCore.SkiaSharpView;
using LiveChartsCore.Defaults;
using TALib;
  

namespace TradingBot.Pages
{
    /// <summary>
    /// Логика взаимодействия для TradingMenu.xaml
    /// </summary>
    public partial class TradingMenu : Page
    {
        bool canStart = false;
        Models.Item Selected = null;

        IReadOnlyList<Candle> History = null;
       
        public TradingMenu()
        {
            InitializeComponent();
        }

        private void loadLists()
        {

            try
            {
                StocksLv.ItemsSource = Db.Items.Where(el => el.TypeId == 1).ToList();
                ExchangeRatesLv.ItemsSource = Db.Items.Where(el => el.TypeId == 2).ToList();
                CryptocurrencyRatesLv.ItemsSource = Db.Items.Where(El => El.TypeId == 3).ToList();

                FavoritesLv.ItemsSource = Db.Items.Where(el => el.UserFav.ClientId == logined.Id).ToList();
                
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
        private async Task loadDataAsync()
        {

            try
            {
                if (canStart && Selected != null)
                {
                    var history = await Yahoo.GetHistoricalAsync(Selected.ShortName, FromDp.SelectedDate.Value.Date, ToDp.SelectedDate.Value.Date, Period.Daily);
                    History = history;
                    ItemsDg.ItemsSource = history.ToList();


                    MainChart.Series = new ISeries[]
                    {
                        new CandlesticksSeries<FinancialPointI>
                        {
                            Values = history.Select(el => new FinancialPointI(Convert.ToDouble(el.High), Convert.ToDouble(el.Open), Convert.ToDouble(el.Close), Convert.ToDouble(el.Low))).ToArray()
                        }
                    };
                    MainChart.XAxes = new[]
                    {
                        new Axis
                        {
                            LabelsRotation = -45,
                            Labels = history.Select(el => el.DateTime.Date.ToString("d")).ToArray()
                        }
                    };
                    
                }
                
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {

            try
            {
                loadLists();
                FromDp.SelectedDate = DateTime.Now.AddMonths(-6);
                ToDp.SelectedDate = DateTime.Now.AddDays(-1);
                ToDp.DisplayDateEnd = DateTime.Now.AddDays(-1);

                canStart = true;
                
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private void FromDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            loadDataAsync();
        }

        private void ToDp_SelectedDateChanged(object sender, SelectionChangedEventArgs e)
        {
            FromDp.DisplayDateEnd = ToDp.SelectedDate;
            if(FromDp.SelectedDate > FromDp.DisplayDateEnd)
            {
                FromDp.SelectedDate = FromDp.DisplayDateEnd;
            }
            loadDataAsync();
        }

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {

            try
            {
                UserFav fav = new UserFav();
                fav.ClientId = logined.Id;

                fav.ItemId = (((sender as CheckBox).Parent as Grid).DataContext as Models.Item).Id;
                Db.UserFavs.Add(fav);
                Db.SaveChanges();
                FavoritesLv.ItemsSource = Db.Items.Where(el => el.UserFav.ClientId == logined.Id).ToList();



            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {

            try
            {
                var it = Db.UserFavs.FirstOrDefault( el => el.ClientId == logined.Id && el.ItemId == (((sender as CheckBox).Parent as Grid).DataContext as Models.Item).Id);
                Db.UserFavs.Remove(it);
                Db.SaveChanges();
                FavoritesLv.ItemsSource = Db.Items.Where(el => el.UserFav.ClientId == logined.Id).ToList();

                if (ListsTC.SelectedIndex == 1)
                {
                    loadLists();
                }
                
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
            
        }

        private void StocksLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(StocksLv.SelectedItem != null)
            {
                Selected = StocksLv.SelectedItem as Models.Item;
                FavoritesLv.SelectedItem = null;
                ExchangeRatesLv.SelectedItem = null;
                CryptocurrencyRatesLv.SelectedItem = null;
                loadDataAsync();
            }
        }

        private void ExchangeRatesLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (ExchangeRatesLv.SelectedItem != null)
            {
                Selected = ExchangeRatesLv.SelectedItem as Models.Item;
                FavoritesLv.SelectedItem = null;
                StocksLv.SelectedItem = null;
                CryptocurrencyRatesLv.SelectedItem = null;
                loadDataAsync();
            }
        }

        private void CryptocurrencyRatesLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (CryptocurrencyRatesLv.SelectedItem != null)
            {
                Selected = CryptocurrencyRatesLv.SelectedItem as Models.Item;
                FavoritesLv.SelectedItem = null;
                StocksLv.SelectedItem = null;
                ExchangeRatesLv.SelectedItem = null;
                loadDataAsync();
            }
        }

        private void FavoritesLv_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (FavoritesLv.SelectedItem != null)
            {
                Selected = FavoritesLv.SelectedItem as Models.Item;
                CryptocurrencyRatesLv.SelectedItem = null;
                StocksLv.SelectedItem = null;
                ExchangeRatesLv.SelectedItem = null;
                loadDataAsync();
            }
        }

        private void Analysis_Click(object sender, RoutedEventArgs e)
        {

            try
            {
                 if(History != null)
                {
                    var diValues = new decimal[History.Count];

                    var highs = History.Select(el => el.High).ToArray();
                    var lows = History.Select(el => el.Low).ToArray();
                    var closes = History.Select(el => el.Close).ToArray();
                    int outBegIdx;

                    int outNbElement;

                    var result = TALib.Core.Rsi(closes, 0, History.Count - 1, diValues, out outBegIdx, out outNbElement, 14);


                    
                    var value = diValues.Last();
                    int lastInd = History.Count - 1;
                    while(value == 0)
                    {
                        lastInd--;
                        value = diValues[lastInd];
                    }


                    if (value > 70)
                    {
                        Info("Тренд возрастёт");
                    }
                    else if(value < 30)
                    {
                        Info("Тренд будет падать");
                    }
                    else
                    {
                        Info("Тренд сохранится");

                    }

                }
            }
            catch (Exception ex)
            {
                Error(ex.Message);
            }
        }
    }
}
