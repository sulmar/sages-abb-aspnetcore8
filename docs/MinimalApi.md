
# Grupowanie tras

``` csharp
 public static class RouteGroupBuilderExtensions
 {
     public static RouteGroupBuilder MapCustomersApi(this RouteGroupBuilder group)
     {
         group.MapGet("/", GetAllCustomers);
         group.MapGet("/{id}", GetCustomer);
         group.MapPost("/", CreateCustomer);
         group.MapPut("/{id}", UpdateCustomer);
         group.MapDelete("/{id}", DeleteCustomer);

         return group;
     }

     private static async Task DeleteCustomer(HttpContext context)
     {
         throw new NotImplementedException();
     }

     private static async Task CreateCustomer(Customer customer, ICustomerRepository repository)
     {
         throw new NotImplementedException();
     }

     private static async Task UpdateCustomer(HttpContext context)
     {
         throw new NotImplementedException();
     }

     private static async Task GetCustomer(HttpContext context)
     {
         throw new NotImplementedException();
     }

     private static async Task GetAllCustomers(HttpContext context)
     {
         throw new NotImplementedException();
     }
 }
```

```csharp
var app = builder.Build();
app.MapGroup("/customers")
        .MapCustomersApi();
```