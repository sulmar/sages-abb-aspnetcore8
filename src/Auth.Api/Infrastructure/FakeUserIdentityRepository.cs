using Auth.Api.Abstractions;
using Auth.Api.Model;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure
{

    public class FakeUserIdentityRepository(IPasswordHasher<UserIdentity> passwordHasher) : IUserIdentityRepository
    {
        public Task<UserIdentity> GetUserIdentityAsync(string username)
        {
            var identity = new UserIdentity
            {
                Username = "marcin",
                FirstName = "Marcin",
                LastName = "Sulecki",
                Email = "marcin.sulecki@sulmar.pl"
            };

            identity.HashedPassword = passwordHasher.HashPassword(identity, "123");
        
            return Task.FromResult(identity);
        }
    }
}
