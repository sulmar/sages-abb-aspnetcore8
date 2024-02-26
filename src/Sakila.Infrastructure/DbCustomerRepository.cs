using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbCustomerRepository(SakilaContext context) : DbEntityRepository<Customer>(context), ICustomerRepository
{
   
}
