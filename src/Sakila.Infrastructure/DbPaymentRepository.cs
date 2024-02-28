using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;
using System.Collections.Immutable;

namespace Sakila.Infrastructure;

public class DbPaymentRepository(SakilaContext context) : DbEntityRepository<Payment>(context), IPaymentRepository
{
    public async Task<List<Payment>> GetByCustomer(int customerId)
    {
        var payments = await entities.Where(p => p.CustomerId == customerId)
            .AsNoTracking()
            .ToListAsync();

        return payments;
    }
}