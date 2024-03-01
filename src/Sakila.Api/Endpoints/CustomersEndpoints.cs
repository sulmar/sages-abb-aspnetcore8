using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Sakila.Api.Services;
using Sakila.Domain.Abstractions;
using Sakila.Domain.Model;

namespace Sakila.Api.Endpoints;
public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomersApi(this RouteGroupBuilder group)
    {
        // GET api/customers/map?lat=51&lng=21
        group.MapGet("/map", async (
            IMapService mapService,
            [FromQuery(Name = "lat")] float latitude,
            [FromQuery(Name = "lng")] float longitude) =>
        {
            mapService.Show(latitude, longitude);
        });

        // [Authorize(Roles = "trainer")]
        // [Authorize(Policy = "isadult")]

        // GET api/customers
        group.MapGet("/", async (ICustomerRepository repository) => await repository.GetAllAsync())
            .RequireAuthorization();

        // GET api/customers/{id}
        group.MapGet("/{id}", async (int id, ICustomerRepository repository) => await repository.GetAsync(id) switch
        {
            Customer customer => Results.Ok(customer),
            _ => Results.NotFound()
        }).WithName("GetCustomerById")
        .Produces<Customer>(StatusCodes.Status200OK)
        .Produces(StatusCodes.Status404NotFound);

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

        group.MapDelete("/{id}", async (int id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetAsync(id);

            if (customer is null)
                return Results.NotFound();

            await repository.RemoveAsync(id);

            return Results.Ok();
        });

        // HEAD api/customers/{id}
        group.MapMethods("/{id}", ["HEAD"], async (int id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetAsync(id);

            if (customer is null)
                return Results.NotFound();

            return Results.Ok();
        });

        return group;
    }
}
