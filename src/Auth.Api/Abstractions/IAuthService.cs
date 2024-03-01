using Auth.Api.Model;

namespace Auth.Api.Abstractions;

public interface IAuthService
{
    Task<AuthorizeResult> AuthorizeAsync(string username, string password);
}


public record AuthorizeResult(bool IsAuthorized, UserIdentity Identity);