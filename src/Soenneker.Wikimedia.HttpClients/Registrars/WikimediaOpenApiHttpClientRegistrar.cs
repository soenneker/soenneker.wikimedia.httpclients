using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Wikimedia.HttpClients.Registrars;

/// <summary>
/// Registers the Wikimedia API HTTP client provider.
/// </summary>
public static class WikimediaOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds the Wikimedia HTTP client provider as a singleton service.
    /// </summary>
    public static IServiceCollection AddWikimediaOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IWikimediaOpenApiHttpClient, WikimediaOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds the Wikimedia HTTP client provider as a scoped service.
    /// </summary>
    public static IServiceCollection AddWikimediaOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IWikimediaOpenApiHttpClient, WikimediaOpenApiHttpClient>();

        return services;
    }
}
