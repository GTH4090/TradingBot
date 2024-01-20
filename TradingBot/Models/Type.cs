using System;
using System.Collections.Generic;

namespace TradingBot.Models;

public partial class Type
{
    public long Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Item> Items { get; set; } = new List<Item>();
}
