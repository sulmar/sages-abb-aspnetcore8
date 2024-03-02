
```csharp
   internal sealed class CustomExceptionHandler : IExceptionHandler
 {
     private readonly ILogger<CustomExceptionHandler> _logger;

     public CustomExceptionHandler(ILogger<CustomExceptionHandler> logger)
     {
         _logger = logger;
     }

     public async ValueTask<bool> TryHandleAsync(
         HttpContext httpContext,
         Exception exception,
         CancellationToken cancellationToken)
     {
         var traceId = Activity.Current?.Id ?? httpContext.TraceIdentifier;

         _logger.LogError(
             exception,
             "Exception occurred: {Message} on {MachineName}. TraceId: {TraceId}",
             exception.Message,
             Environment.MachineName,
             traceId
             );

         //await Results.Problem(
         //    title: "Server error",
         //    statusCode: StatusCodes.Status500InternalServerError)
         //    .ExecuteAsync(httpContext);

         var (statusCode, title) = MapException(exception);

         await Results.Problem(
             title: title,
             statusCode: statusCode,
             extensions: new Dictionary<string, object?>
             {
                 ["traceId"] = traceId
             }
             )
             .ExecuteAsync(httpContext);

         return true;
     }

     private static (int StatusCode, string Title) MapException(Exception exception)
     {
         return exception switch
         {
             ArgumentOutOfRangeException => (StatusCodes.Status400BadRequest, "Bad Request"),
             _ => (StatusCodes.Status500InternalServerError, "Internal Server Error")
         };
     }
 }
```

```csharp
var builder = WebApplication.CreateBuilder();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

app.UseExceptionHandler("/Error");

app.MapGet("/exception", () => { throw new Exception("Hello Exception"); });

```