using System;
using System.Collections.Generic;

namespace ElectronicsStore.Models;

public partial class CartDetail
{
    public int IdCartDetail { get; set; }

    public int IdCart { get; set; }

    public int IdProduct { get; set; }

    public int Count { get; set; }

    public virtual Cart IdCartNavigation { get; set; } = null!;

    public virtual Product IdProductNavigation { get; set; } = null!;
}
