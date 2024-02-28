using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Domain.SearchCritierias;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbFilmRepository(SakilaContext context) : DbEntityRepository<Film>(context), IFilmRepository
{
    public async Task<IEnumerable<Film>> GetByAsync(FilmSearchCritieria critieria)
    {
        var query = entities.AsQueryable();

        if (!string.IsNullOrEmpty(critieria.Title))
            query = query.Where(f => f.Title.Contains(critieria.Title));

        if (!string.IsNullOrEmpty(critieria.Rating))
            query = query.Where(f => f.Rating.Equals(critieria.Rating));


        var films = await query.AsNoTracking().ToListAsync();

        return films;
    }

    public async Task<IEnumerable<Film>> GetByCategoryAsync(string category)
    {
        var films = await entities
            .SelectMany(e => e.FilmCategories)
            .Where(c => c.Category.Name == category)
            .Select(fc => fc.Film).ToListAsync();

        return films;
    }
}
