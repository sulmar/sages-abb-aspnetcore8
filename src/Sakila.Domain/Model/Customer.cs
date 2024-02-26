using System;
using System.Collections.Generic;

namespace Sakila.Domain.Model;

public partial class Customer : BaseEntity
{
    public int CustomerId { get; set; }

    public int StoreId { get; set; }

    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public string? Email { get; set; }

    public int AddressId { get; set; }

    public string Active { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public DateTime LastUpdate { get; set; }

    public virtual Address Address { get; set; } = null!;

    public virtual ICollection<Payment> Payments { get; set; } = new List<Payment>();

    public virtual ICollection<Rental> Rentals { get; set; } = new List<Rental>();
    
    public virtual Store Store { get; set; } = null!;
}
