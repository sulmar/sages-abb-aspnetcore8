namespace Sakila.Api.Middlewares;


public static class LoggerMiddlewareExtensions
{
    public static IApplicationBuilder UseLogger(this IApplicationBuilder app)
    {
        app.UseMiddleware<LoggerMiddleware>();

        return app;
    }
}

public class LoggerMiddleware
{
    private readonly RequestDelegate next;
    private readonly ILogger<LoggerMiddleware> logger;

    public LoggerMiddleware(RequestDelegate next, ILogger<LoggerMiddleware> logger)
    {
        this.next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        logger.LogInformation("{Method} {Path}", context.Request.Method, context.Request.Path);

        await next(context);

        logger.LogInformation($"{context.Response.StatusCode}");
    }
}
