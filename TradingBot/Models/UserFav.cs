using System;
using System.Collections.Generic;

namespace TradingBot.Models;

public partial class UserFav
{
    public long Id { get; set; }

    public long ClientId { get; set; }

    public long ItemId { get; set; }

    public virtual Client Client { get; set; } = null!;

    public virtual Item Item { get; set; } = null!;
}
