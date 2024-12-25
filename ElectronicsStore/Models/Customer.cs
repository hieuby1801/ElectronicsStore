using System;
using System.Collections.Generic;

namespace ElectronicsStore.Models;

public partial class Customer
{
    public int IdCustomer { get; set; }

    public string DisplayName { get; set; } = null!;

    public string Phone { get; set; } = null!;

    public string Address { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Password { get; set; } = null!;

    public virtual ICollection<Cart> Carts { get; set; } = new List<Cart>();
}
