using System;
using System.Collections.Generic;

namespace TradingBot.Models;

public partial class Item
{
    public long Id { get; set; }

    public string FullName { get; set; } = null!;

    public string ShortName { get; set; } = null!;

    public long TypeId { get; set; }

    public long? IsFav { get; set; }

    public virtual Type Type { get; set; } = null!;

    public virtual UserFav? UserFav { get; set; }
}
