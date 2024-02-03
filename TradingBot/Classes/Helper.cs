using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using TradingBot.Models;

namespace TradingBot.Classes
{
    public class Helper
    {
        public static TradingBotDbContext Db = new TradingBotDbContext();

        public static Client logined = null;

        public static void Error(string message = "Ошибка БД")
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        public static void Info(string message)
        {
            MessageBox.Show(message, "Инфо", MessageBoxButton.OK, MessageBoxImage.Information);
        }
    }
}
