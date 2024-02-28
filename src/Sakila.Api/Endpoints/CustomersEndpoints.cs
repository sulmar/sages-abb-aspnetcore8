using Sakila.Domain.Abstractions;

namespace Sakila.Api.Endpoints;
public static class CustomersEndpoints
{
    public static RouteGroupBuilder MapCustomersApi(this RouteGroupBuilder group)
    {
        // GET api/customers
        group.MapGet("/", async (ICustomerRepository repository) => await repository.GetAllAsync());

        // GET api/customers/{id}
        group.MapGet("/{id}", async (int id, ICustomerRepository repository) =>
        {
            var customer = await repository.GetAsync(id);

            if (customer is null)
            {
                return Results.NotFound();
            }

            return Results.Ok(customer);
        });

        return group;
    }
}
