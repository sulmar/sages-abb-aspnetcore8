using Sakila.Domain.Model;
using Sakila.Domain.SearchCritierias;

namespace Sakila.Domain.Abstractions;

public interface IFilmRepository : IEntityRepository<Film>
{
    Task<IEnumerable<Film>> GetByCategoryAsync(string category);

    Task<IEnumerable<Film>> GetByAsync(FilmSearchCritieria critieria);
}
