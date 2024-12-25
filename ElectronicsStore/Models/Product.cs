using System;
using System.Collections.Generic;

namespace ElectronicsStore.Models;

public partial class Product
{
    public int IdProduct { get; set; }

    public string Name { get; set; } = null!;

    public double Price { get; set; }

    public int IdCategory { get; set; }

    public string? Image { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Category IdCategoryNavigation { get; set; } = null!;
}
