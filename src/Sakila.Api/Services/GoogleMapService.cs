using Microsoft.Extensions.Options;

namespace Sakila.Api.Services;

public interface IMapService
{
    void Show(float lat, float lng);
}

public class GoogleMapServiceOptions
{
    public string Url { get; set; }
    public int Zoom { get; set; }
}

public class GoogleMapService(ILogger<GoogleMapService> logger,
    IOptions<GoogleMapServiceOptions> options) : IMapService
{
    public void Show(float lat, float lng)
    {
        logger.LogInformation("Get request {url} {zoom} {lat},{lng}", 
            options.Value.Url, options.Value.Zoom, lat, lng);
    }
}
