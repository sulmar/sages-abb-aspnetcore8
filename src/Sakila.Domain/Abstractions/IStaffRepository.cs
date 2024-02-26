using Sakila.Domain.Model;

namespace Sakila.Domain.Abstractions;

public interface IStaffRepository : IEntityRepository<Staff>
{
    Task<Staff?> GetByUsername(string username);
}