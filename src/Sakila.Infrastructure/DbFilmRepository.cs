using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbFilmRepository(SakilaContext context) : DbEntityRepository<Film>(context), IFilmRepository
{
    public async Task<IEnumerable<Film>> GetByAsync(string? title, string? rating)
    {
        var query = entities.AsQueryable();

        if (!string.IsNullOrEmpty(title))
            query = query.Where(f => f.Title.Contains(title));

        if (!string.IsNullOrEmpty(rating))
            query = query.Where(f => f.Rating.Equals(rating));


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
