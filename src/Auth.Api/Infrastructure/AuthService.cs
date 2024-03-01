using Auth.Api.Abstractions;
using Auth.Api.Model;
using Microsoft.AspNetCore.Identity;

namespace Auth.Api.Infrastructure;

public class AuthService(
    IUserIdentityRepository repository,
    IPasswordHasher<UserIdentity> passwordHasher
    ) : IAuthService
{
    public async Task<AuthorizeResult> AuthorizeAsync(string username, string password)
    {
        var identity = await repository.GetUserIdentityAsync(username);

        var result = passwordHasher.VerifyHashedPassword(identity,
            identity.HashedPassword, password);

        if (result == PasswordVerificationResult.Success)
            return new AuthorizeResult(true, identity);
        else
            return new AuthorizeResult(false, new Model.UserIdentity());
    }
}
