using Microsoft.AspNetCore.Http.HttpResults;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;

namespace Sakila.Api.Endpoints;
public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomersApi(this RouteGroupBuilder group)
    {
        // GET api/customers
        group.MapGet("/", async (ICustomerRepository repository) => await repository.GetAllAsync());

        // GET api/customers/{id}
        group.MapGet("/{id}", async (int id, ICustomerRepository repository) => await repository.GetAsync(id) switch
        {
            Customer customer => Results.Ok(customer),
            _ => Results.NotFound()
        }).WithName("GetCustomerById");

        // POST api/customers
        group.MapPost("/", async (Customer customer, ICustomerRepository repository) =>
        {
            await repository.AddAsync(customer);

            return Results.CreatedAtRoute("GetCustomerById", new { Id = customer.CustomerId, customer });
        });

        // PUT api/customers/{id}

        group.MapPut("/{id}", async (int id, Customer customer, ICustomerRepository repository) =>
        {
            if (id != customer.CustomerId)
                return Results.BadRequest();

            await repository.UpdateAsync(customer);

            return Results.NoContent();
        });

        return group;
    }
}
