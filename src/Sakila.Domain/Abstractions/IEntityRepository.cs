using Sakila.Domain.Model;

namespace Sakila.Domain.Abstractions;

public interface IEntityRepository<T>
    where T : BaseEntity    
{
    Task<IEnumerable<T>> GetAllAsync(); 
    ValueTask<T?> GetAsync(int id);
    Task AddAsync(T item);
    Task RemoveAsync(int id);
}

