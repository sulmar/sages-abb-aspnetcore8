namespace Sakila.Api.Middlewares;

public class AuthorizeMiddleware(RequestDelegate next)
{
    public async Task InvokeAsync(HttpContext context)
    {
        var authorizeSecretKey = context.Request.Headers["x-secret-key"];

        if (authorizeSecretKey.ToString() != "abb")
        {
            context.Response.StatusCode = StatusCodes.Status401Unauthorized;
            return;
        }

        await next(context);
    }
}
