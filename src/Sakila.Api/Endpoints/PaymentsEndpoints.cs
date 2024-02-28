using Sakila.Domain.Abstractions;

namespace Sakila.Api.Endpoints;

public static class PaymentsEndpoints
{
    public static RouteGroupBuilder MapPaymentsApi(this RouteGroupBuilder group)
    {
        // GET api/customers/{id}/payments
        group.MapGet("~/api/customers/{id}/payments", async (int id, IPaymentRepository repository) => await repository.GetByCustomer(id));

        // GET api/payments
        group.MapGet("/", async (IPaymentRepository repository) => await repository.GetAllAsync());


        return group;


    }
}