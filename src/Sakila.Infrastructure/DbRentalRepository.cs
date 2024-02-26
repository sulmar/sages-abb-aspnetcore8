using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbRentalRepository(SakilaContext context) : DbEntityRepository<Rental>(context), IRentalRepository
{
}
