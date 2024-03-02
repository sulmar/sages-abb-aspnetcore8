``` csharp
app.MapGet("/", () => Results.Redirect("/swagger"))
    .ExcludeFromDescription();

app.MapGet("api/ping", () => Results.Ok())
    .ExcludeFromDescription();
```


```csharp
app.MapGet("/customers", (ICustomerRepository customerRepository) => customerRepository.Get());
```

```csharp
app.MapGet("/customers/{id}", (int id, ICustomerRepository repository) => repository.Get(id) switch
{
    Customer customer => Results.Ok(customer),
    null => Results.NotFound()
})
    .Produces<Customer>(StatusCodes.Status200OK)
    .Produces(StatusCodes.Status404NotFound);
```

``` csharp
app.MapGet("api/products/{code}", async (string code, string? currencyCode, IProductService service) => await service.GetByCode(code, currencyCode) switch
{
    Product product => Results.Ok(product),
    _ => Results.NotFound()
}).CacheOutput(x=>x.AddPolicy<OutputCacheWithAuthPolicy>())
  .RequireAuthorization()
  .WithName("GetProduct")
  .WithOpenApi(operation =>
  {
      operation.Summary = "Get product by code";      

      return operation;
  })
  .Produces<Product>(StatusCodes.Status200OK)
  .Produces(StatusCodes.Status404NotFound);
```
# Wyrażenia regularne

```csharp
app.MapGet("/films/{rating:regex(^\\b(PG|PG-13|G|R)\\b)}", (string rating) => $"Hello rating {rating}");
```


# Przekazywanie parametrów

```csharp
app.MapPost("api/carts", async (
    [FromHeader(Name = "x-session-id")] string id,
    [FromBody] Rental rental, 
    [FromServices] IRnetalRepository repository) =>
{    
    await repository.AddAsync(rental);

    return Results.Ok();
});

```


# Przerywanie obwodu

_Routing zwarciowy_ to nowa funkcja w platformie .NET 8. Jest stosowana do co najmniej jednego punktu końcowego w aplikacji i oznacza, że ​​punkt końcowy koncepcyjnie „pomija” oprogramowanie pośredniczące między `RoutingMiddleware`i `EndpointMiddleware`.


 Głównym przypadkiem użycia jest zmniejszenie narzutu na żądania, o których wiesz, że _albo_ zwrócą `404`odpowiedź, albo o których _wiesz, że_ nigdy nie będą potrzebować autoryzacji ani CORS. Przykładami mogą być dobrze znane adresy URL, których przeglądarki żądają automatycznie, lub inne standardowe ścieżki. Na przykład:

- `/robots.txt`— mówi programom zbierającym informacje, takim jak Google, co mają indeksować.
- `/favicon.ico`—Ikona karty w przeglądarce, której przeglądarka żąda automatycznie.
- `/.well-known/*`(wszystkie ścieżki poprzedzone `.well-known/`) — używane w różnych specyfikacjach, takich jak [OpenID Connect Connect](https://openid.net/specs/openid-connect-discovery-1_0.html) , [security.txt](https://en.wikipedia.org/wiki/Security.txt) lub [webfinger](https://en.wikipedia.org/wiki/WebFinger) .
- 
```csharp
app.MapGet("/", () => "Hello World!")
   .ShortCircuit(); // 👈 Add this
```

Spowoduje to dodanie metadanych zwarcia do punktu końcowego i oznacza, że ​​pojawi się komunikat „Hello World!” punkt końcowy zostanie wykonany w `RoutingMiddleware`zamiast w `EndpointMiddleware`.

Opcjonalnie możesz także podać kod statusu, który zostanie automatycznie ustawiony w odpowiedzi:

```csharp
app.MapGet("/", () => "Hello World!")
   .ShortCircuit(201); // 👈 Sets the status code to 201
```

