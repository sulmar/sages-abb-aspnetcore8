
# Rejestracja diagnostyki


```csharp

  public class MachineHealthCheck : IHealthCheck
    {
        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            return Task.FromResult(HealthCheckResult.Healthy());
        }
    }

builder.Services.AddHealthChecks()
    .AddCheck<MachineHealthCheck>("Machine")
    .AddCheck("Ping", () => HealthCheckResult.Healthy())
    .AddCheck("Random", () =>
    {
        if (DateTime.Now.Minute % 2 == 0)        
            return HealthCheckResult.Healthy();
        else
            return HealthCheckResult.Unhealthy();
    });

var app = builder.Build();

app.MapHealthChecks("/hc", new HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
```

```csharp
builder.Services.AddHealthChecks()
    .AddSqlServer(
        builder.Configuration.GetConnectionString("SakilaConnection"), name: "Sakila Db", tags: new[] { "database" }); ;
```

# Utworzenie własnej diagnostyki 