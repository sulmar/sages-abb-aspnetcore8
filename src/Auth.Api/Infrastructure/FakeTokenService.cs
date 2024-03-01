using Auth.Api.Abstractions;
using Auth.Api.Model;

namespace Auth.Api.Infrastructure;

public class FakeTokenService : ITokenService
{
    public string CreateAccessToken(UserIdentity identity) => "abc";
}
