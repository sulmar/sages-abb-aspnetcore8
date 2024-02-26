using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;
using Sakila.Intrastructure;

namespace Sakila.Infrastructure;

public class DbStaffRepository(SakilaContext context) : DbEntityRepository<Staff>(context), IStaffRepository
{
    public async Task<Staff?> GetByUsername(string username)
    {
        return await entities.SingleOrDefaultAsync(e => e.Username == username);
    }
}