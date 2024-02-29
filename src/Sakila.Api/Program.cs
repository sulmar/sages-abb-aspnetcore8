using Json = Microsoft.AspNetCore.Http.Json;
using Sakila.Domain.Abstractions;
using System.Text.Json.Serialization;
using Sakila.Api.Endpoints;
using Sakila.Api.Extensions;
using Sakila.Domain.SearchCritierias;
using Microsoft.AspNetCore.Mvc;
using Sakila.Domain.Model;
using Sakila.Api.Middlewares;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbRepositories();

builder.Services.Configure<Json.JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});


// dotnet add package Swashbuckle.AspNetCore
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseLogger();
app.UseMiddleware<AuthorizeMiddleware>();

app.UseSwagger();
app.UseSwaggerUI();


app.UseRouting();

//app.MapGet("/", ([FromHeader(Name = "x-code")] string? code) => $"Hello {code}!!!");

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
