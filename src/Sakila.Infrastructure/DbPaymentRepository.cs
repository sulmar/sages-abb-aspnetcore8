using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbPaymentRepository(SakilaContext context) : DbEntityRepository<Payment>(context), IPaymentRepository
{
}