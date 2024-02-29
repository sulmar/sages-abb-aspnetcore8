namespace Sakila.Api.Middlewares;

public class LoggerMiddleware
{
    private readonly RequestDelegate next;

    public LoggerMiddleware(RequestDelegate next)
    {
        this.next = next;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        Console.WriteLine($"{context.Request.Method} {context.Request.Path}");

        await next(context);

        Console.WriteLine($"{context.Response.StatusCode}");
    }
}
