using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http.Json;
using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Infrastructure;
using Sakila.Intrastructure;
using System.Text.Json.Serialization;
using Sakila.Api.Endpoints;
using Sakila.Api.Extensions;
using Sakila.Domain.SearchCritierias;

var builder = WebApplication.CreateBuilder(args);

builder.AddDbRepositories();

builder.Services.Configure<JsonOptions>(options =>
{
    options.SerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.SerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

var app = builder.Build();

app.MapGet("/", () => "Hello ABB!!!");

app.MapGet("/ping", () => "pong");

app.MapGroup("api/customers")
    .MapCustomersApi();

app.MapGroup("api/payments")
    .MapPaymentsApi();

// GET api/films?title=Lorem&rating=PG
app.MapGet("api/films", ([AsParameters] FilmSearchCritieria critieria, IFilmRepository repository) => repository.GetByAsync(critieria));


app.Run();
