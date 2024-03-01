using Auth.Api.Model;

namespace Auth.Api.Abstractions;

public interface IUserIdentityRepository
{
    Task<UserIdentity> GetUserIdentityAsync(string username);
}
