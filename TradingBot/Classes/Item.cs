using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TradingBot.Models
{
    public partial class Item
    {
        public bool Favorite
        {
            get
            {
                if (IsFav == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            
        }
    }
}
