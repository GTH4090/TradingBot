using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static TradingBot.Classes.Helper;

namespace TradingBot.Models
{
    public partial class Item
    {
        public bool Favorite
        {
            get
            {
                
                if(logined != null)
                {
                    if(Db.UserFavs.Where(el => el.ClientId == logined.Id).FirstOrDefault(el => el.ItemId == this.Id) != null)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return false;
                }
            }
            
        }
    }
}
