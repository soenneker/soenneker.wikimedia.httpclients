using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Registrar;

namespace Soenneker.Wikimedia.HttpClients.Registrars;

/// <summary>
/// Registers the OpenAPI HttpClient wrapper for dependency injection.
/// </summary>
public static class WikimediaOpenApiHttpClientRegistrar
{
    /// <summary>
    /// Adds <see cref="WikimediaOpenApiHttpClient"/> as a singleton service. <para/>
    /// </summary>
    public static IServiceCollection AddWikimediaOpenApiHttpClientAsSingleton(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddSingleton<IWikimediaOpenApiHttpClient, WikimediaOpenApiHttpClient>();

        return services;
    }

    /// <summary>
    /// Adds <see cref="WikimediaOpenApiHttpClient"/> as a scoped service. <para/>
    /// </summary>
    public static IServiceCollection AddWikimediaOpenApiHttpClientAsScoped(this IServiceCollection services)
    {
        services.AddHttpClientCacheAsSingleton()
                .TryAddScoped<IWikimediaOpenApiHttpClient, WikimediaOpenApiHttpClient>();

        return services;
    }
}
