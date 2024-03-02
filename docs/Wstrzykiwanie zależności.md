
# Rejestracja usług

# Porównanie cykli życia usługi

1. **Singleton**:
    
    - W przypadku singletona tylko jedna instancja obiektu jest tworzona na cały czas życia aplikacji.
    - Oznacza to, że ta sama instancja będzie ponownie używana we wszystkich klasach, które od niej zależą.
    - Obiekty singletonowe zazwyczaj są tworzone przy uruchamianiu aplikacji i usuwane przy jej zamykaniu.
    - Obiekty singletonowe są współdzielone między całym zakresem aplikacji, co może być przydatne dla obiektów reprezentujących usługi bezstanowe lub zasoby, które można bezpiecznie udostępnić.
2. **Transient**:
    
    - W przypadku transientu nowa instancja obiektu jest tworzona za każdym razem, gdy jest ona żądana.
    - Oznacza to, że za każdym razem, gdy komponent wymaga instancji obiektu o czasie życia transient, tworzona jest nowa instancja i zwracana.
    - Obiekty transient zazwyczaj mają krótki czas życia i nie są współdzielone między różnymi komponentami.
    - Obiekty transient są przydatne dla obiektów, które są lekkie, bezstanowe lub muszą utrzymać krótki czas życia.
3. **Scoped**:
    
    - W przypadku scoped nowa instancja obiektu jest tworzona raz na żądanie lub zakres.
    - Obiekty scoped istnieją w określonym zakresie, takim jak żądanie sieciowe w ASP.NET Core.
    - W obrębie tego samego zakresu ta sama instancja obiektu jest zwracana dla wszystkich żądań.
    - Jednak różne zakresy otrzymają różne instancje obiektu.
    - Obiekty scoped są przydatne w przypadkach, gdy chcesz utrzymać stan w obrębie pojedynczego żądania lub operacji, na przykład konteksty bazy danych w aplikacjach sieciowych.

| Aspekt     | Singleton                  | Transient                   | Scoped                        |
|------------|----------------------------|-----------------------------|-------------------------------|
| Instancje  | Jedna na całą aplikację   | Jedna na żądanie            | Jedna na zakres/żądanie      |
| Powtórne użycie | Współdzielony w całej aplikacji | Niewspółdzielony | Współdzielony w obrębie zakresu/żądania |
| Czas życia | Czas życia aplikacji       | Krótkotrwały                | Czas życia zakresu/żądania   |
| Przykłady zastosowań | Usługi stanowe, współdzielone zasoby | Lekkie, bezstanowe obiekty | Obiekty stanowe w zakresie |

# Pobieranie usługi na podstawie typu
# Pobieranie usługi na podstawie klucza

```csharp
public interface INotificationService
{
    void Send(string message);
}

public class SmsNotificationService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"Send sms {message}");
}

public class EmailNotificationService : INotificationService
{
    public void Send(string message) => Console.WriteLine($"Send email {message}");
}
```


```csharp
var builder = WebApplication.CreateBuilder();

builder.Services.AddKeyedSingleton<INotificationService, SmsNotificationService>("sms");
builder.Services.AddKeyedSingleton<INotificationService, EmailNotificationService>("email");

```

```csharp
app.MapGet("/sms", ([FromKeyedServices("sms")] INotificationService notifier) => notifier.Send("Hello world"));
app.MapGet("/email", ([FromKeyedServices("email")] INotificationService notifier) => notifier.Send("Hello world"));

```

## Przykład

``` csharp
public interface IPaymentProcessor
{
   void ProcessPayment(decimal amount);
}

public class PayPalProcessor : IPaymentProcessor 
{ 
    public void ProcessPayment(decimal amount) => Console.WriteLine($"Processing PayPal  payment: {amount}");
}

public class StripeProcessor : IPaymentProcessor 
{
public void ProcessPayment(decimal amount) => Console.WriteLine($"Processing Stripe  payment: {amount}");
}


public class Order
{
    public decimal Amount { get; set; }
    public PaymentMethod PaymentMethod { get; set; }
}

public enum PaymentMethod
{
    PayPal,
    Stripe
}

```


```csharp

builder.Services.AddKeyedTransient<IPaymentProcessor, PayPalProcessor>("PayPal");
builder.Services.AddKeyedTransient<IPaymentProcessor, StripeProcessor>("Stripe");

app.MapGet("/orders/{orderId}", (IServiceProvider serviceProvider, int orderId) =>
{
    var order = new Order { PaymentMethod = PaymentMethod.PayPal };

    var paymentProcessor = serviceProvider.GetRequiredKeyedService<IPaymentProcessor>(order.PaymentMethod.ToString());

   paymentProcessor.ProcessPayment(order.Amount);
   
    return $"Payment processed";

});
```