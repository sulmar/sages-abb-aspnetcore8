using Auth.Api.Abstractions;
using Auth.Api.Infrastructure;
using Auth.Api.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IPasswordHasher<UserIdentity>, PasswordHasher<UserIdentity>>();

builder.Services.AddTransient<IAuthService, AuthService>();
builder.Services.AddTransient<IUserIdentityRepository, FakeUserIdentityRepository>();
builder.Services.AddTransient<ITokenService, FakeTokenService>();

var app = builder.Build();


app.MapGet("/", () => "Hello Auth.Api!");

// POST /api"/token/create
app.MapPost("/api/token/create", async ([FromForm] LoginModel model,
    IAuthService authService,
    ITokenService tokenService
    ) =>
{
    var result = await authService.AuthorizeAsync(model.Username, model.Password);

    if (result.IsAuthorized)
    {
        var accessToken = tokenService.CreateAccessToken(result.Identity);

        return Results.Ok(accessToken);
    }

    return Results.Unauthorized();

 }).DisableAntiforgery();

app.Run();
