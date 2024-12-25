using System;
using System.Collections.Generic;

namespace ElectronicsStore.Models;

public partial class Cart
{
    public int IdCart { get; set; }

    public int IdCustomer { get; set; }

    public virtual ICollection<CartDetail> CartDetails { get; set; } = new List<CartDetail>();

    public virtual Customer IdCustomerNavigation { get; set; } = null!;
}
