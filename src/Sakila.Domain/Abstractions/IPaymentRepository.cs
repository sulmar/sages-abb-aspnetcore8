using Sakila.Domain.Model;

namespace Sakila.Domain.Abstractions;

public interface IPaymentRepository : IEntityRepository<Payment>
{
    Task<List<Payment>> GetByCustomer(int customerId);
}
