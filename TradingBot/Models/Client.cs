using System;
using System.Collections.Generic;

namespace TradingBot.Models;

public partial class Client
{
    public long Id { get; set; }

    public string Login { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<UserFav> UserFavs { get; set; } = new List<UserFav>();
}
