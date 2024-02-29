using Microsoft.EntityFrameworkCore;
using Sakila.Domain.Abstractions;
using Sakila.Infrastructure;
using Sakila.Intrastructure;

namespace Sakila.Api.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static WebApplicationBuilder AddDbRepositories(this WebApplicationBuilder builder)
    {
        var connectionString = builder.Configuration.GetConnectionString("SakilaConnection");

        builder.Services.AddScoped<ICustomerRepository, DbCustomerRepository>();
        builder.Services.AddScoped<IPaymentRepository, DbPaymentRepository>();
        builder.Services.AddScoped<IFilmRepository, DbFilmRepository>();
        builder.Services.AddScoped<IActorRepository, DbActorRepository>();

        builder.Services.AddDbContextPool<SakilaContext>(options =>
        {
            options.UseSqlServer(connectionString);
        });

        return builder;
    }
}
