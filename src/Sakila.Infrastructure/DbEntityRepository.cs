using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbEntityRepository<T>(SakilaContext context) : IEntityRepository<T>
    where T : BaseEntity, new()
{
    protected DbSet<T> entities => context.Set<T>();

    public async Task AddAsync(T item)
    {
        await entities.AddAsync(item);

        await context.SaveChangesAsync();
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync() => await entities.AsNoTracking().ToListAsync();

    public async ValueTask<T?> GetAsync(int id) => await entities.FindAsync(id);

    public async Task RemoveAsync(int id)
    {
        var item = await GetAsync(id);

        entities.Remove(item!);

        await context.SaveChangesAsync();
    }

    public async Task UpdateAsync(T item)
    {
        entities.Update(item);

        await context.SaveChangesAsync();
    }
}
