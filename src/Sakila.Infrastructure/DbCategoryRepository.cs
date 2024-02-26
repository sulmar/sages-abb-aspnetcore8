using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbCategoryRepository(SakilaContext context) : DbEntityRepository<Category>(context), ICategoryRepository
{ 
}