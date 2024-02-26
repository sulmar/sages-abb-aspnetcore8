using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Model;

namespace Sakila.Intrastructure;

public partial class SakilaContext
{
    partial void OnModelCreatingPartial(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Rental>()
            .Navigation(p => p.Customer)
            .AutoInclude();
    }
}
