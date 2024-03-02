
# CreateBuilder


```csharp
var builder = WebApplication.CreateBuilder(args);
```
https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/generic-host?view=aspnetcore-8.0#default-builder-settings

# CreateSlimBuilder

```csharp
var builder = WebApplication.CreateSlimBuilder(args);
```

Metoda `CreateSlimBuilder` jest podobna do `CreateBuilder`. Obie metody inicjalizują `WebApplicationBuilder`, ale `CreateSlimBuilder` inicjalizuje tylko minimalny zestaw funkcji, który jest niezbędny do uruchomienia aplikacji. Oznacza to, że wiele funkcji jest pominiętych:
- Brak wsparcia dla startup assemblies](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/host/platform-specific-configuration?view=aspnetcore-8.0) (`IHostingStartup`)
- Brak obsługi `UseStartup<Startup>`
- Usunięto niektórych dostawców logowania
	- EventLog
	- Debug
	- EventSource
- Usunięto następujące funkcje hostingu
	- Brak obsługi `UseStaticWebAssets` dla ładowania statycznych plików z zewnętrznych projektów i pakietów
	- Brak integracji z IIS
- Usunięto następujące funkcje z konfiguracji Kestrel
	- Brak obsługi HTTPS
	- Brak obsługi Quic (HTTP/3)
- Brak obsługi reguł w trasach _Regex_ i _alpha_

```csharp
public sealed class WebApplication : IHost, IApplicationBuilder, IEndpointRouteBuilder, IAsyncDisposable
{
    public static WebApplicationBuilder CreateBuilder() =>
        new WebApplicationBuilder(new WebApplicationOptions());

    public static WebApplicationBuilder CreateSlimBuilder() =>
        new WebApplicationBuilder(new WebApplicationOptions(), slim: true);
                                                            // 👆 the only difference
}
```


# EnableRequestDelegateGenerator
 
```xml
<Project Sdk="Microsoft.NET.Sdk.Web">

  <PropertyGroup>
    <TargetFramework>net8.0</TargetFramework>
    <Nullable>enable</Nullable>
    <ImplicitUsings>enable</ImplicitUsings>
    <EnableRequestDelegateGenerator>true</EnableRequestDelegateGenerator>
  </PropertyGroup>
    
</Project>
```

W Dependencies Analyzers Microsoft.AspNetCore.Http.RequestDelegateGenerator pojawi się wygenerowany kod. 