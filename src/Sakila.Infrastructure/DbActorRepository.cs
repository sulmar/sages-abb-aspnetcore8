using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbActorRepository(SakilaContext context) : DbEntityRepository<Actor>(context), IActorRepository
{

}