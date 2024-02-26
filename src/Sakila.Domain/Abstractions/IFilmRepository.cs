using Sakila.Domain.Model;

namespace Sakila.Domain.Abstractions;

public interface IFilmRepository : IEntityRepository<Film>
{
    Task<IEnumerable<Film>> GetByCategoryAsync(string category);

    Task<IEnumerable<Film>> GetByAsync(string? title, string? rating);
}
