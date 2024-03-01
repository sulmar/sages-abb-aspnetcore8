using Json = Microsoft.AspNetCore.Http.Json;
using Sakila.Domain.Abstractions;
using System.Text.Json.Serialization;
using Sakila.Api.Endpoints;
using Sakila.Api.Extensions;
using Sakila.Domain.SearchCritierias;
using Microsoft.AspNetCore.Mvc;
using Sakila.Domain.Model;
using Sakila.Api.Middlewares;
using Serilog;
using Serilog.Formatting.Compact;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using HealthChecks.UI.Client;
using Microsoft.Extensions.DependencyInjection;
using Sakila.Api.Services;

var builder = WebApplication.CreateBuilder(args);

string env = builder.Environment.EnvironmentName;
builder.Configuration.AddJsonFile($"appsettings.{env}.json", optional: true);
builder.Configuration.AddJsonFile($"appsettings.{env}.ziemowit.json", optional: true);

builder.Configuration.AddXmlFile("appsettings.xml", optional: true);

string? googleMapUrl = builder.Configuration["GoogleMap:Url"];

builder.Services.AddTransient<IMapService, GoogleMapService>();
builder.Services.Configure<GoogleMapServiceOptions>(builder.Configuration.GetSection("GoogleMap"));

builder.AddDbRepositories();

builder.Services.Configure<Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// dotnet add package Swashbuckle.AspNetCore
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Logging.ClearProviders();

var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("log.txt")
    .WriteTo.File(new CompactJsonFormatter(), "log.json")
    .CreateLogger();

builder.Logging.AddSerilog(logger);

builder.Services.AddHealthChecks()
    .AddSqlServer(builder.Configuration.GetConnectionString("SakilaConnection") , name: "SakilaDb-check")
    .AddCheck("Ping", () => HealthCheckResult.Healthy())
    .AddCheck("Random", () =>
    {
        if (DateTime.Now.Minute % 2 == 0)
        {
            return HealthCheckResult.Healthy();
        }
        else
        {
            return HealthCheckResult.Unhealthy();
        }
    })
    ;

builder.Services
    .AddHealthChecksUI(options =>
    {
        options.AddHealthCheckEndpoint("Healthcheck API", "/hc");
    })
    .AddInMemoryStorage();

var app = builder.Build();

app.MapHealthChecks("/hc", new  HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
   });

app.UseLogger();
// app.UseMiddleware<AuthorizeMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();

//app.MapGet("/", ([FromHeader(Name = "x-code")] string? code) => $"Hello {code}!!!");
app.MapHealthChecksUI(options => options.UIPath = "/dashboard");

app.MapGet("/", () =>   Results.Redirect("/swagger"));

app.MapGet("/ping", () => "pong");

app.MapGroup("api/customers")
    .MapCustomersApi();

app.MapGroup("api/payments")
    .MapPaymentsApi();


// dotnet add package Microsoft.AspNetCore.OpenApi
// GET api/films?title=Lorem&rating=PG
app.MapGet("api/films", ([AsParameters] FilmSearchCritieria critieria, IFilmRepository repository) => repository.GetByAsync(critieria))
      .WithOpenApi(operation => new(operation)
      {
          Summary = "This is a summary",
          Description = "This is a description"
      });

app.MapPost("api/photos", (IFormFile file) =>
{
    return Results.Ok();
}).DisableAntiforgery();

app.MapGet("/Hello", async (HttpRequest req, HttpResponse res) =>
{
    await res.WriteAsync("Hello World!");
    res.StatusCode = 200;
});


app.MapPost("api/actors", async ([FromForm] Actor actor, IActorRepository repository) =>
{
    await repository.AddAsync(actor);
});

app.Run();
