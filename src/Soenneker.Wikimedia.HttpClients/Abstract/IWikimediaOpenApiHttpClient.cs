using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Threading;

namespace Soenneker.Wikimedia.HttpClients.Abstract;

/// <summary>
/// Provides a cached <see cref="HttpClient"/> configured for the Wikimedia API.
/// </summary>
public interface IWikimediaOpenApiHttpClient : IDisposable, IAsyncDisposable
{
    /// <summary>
    /// Gets the configured Wikimedia API client.
    /// </summary>
    /// <param name="cancellationToken">The cancellation token.</param>
    /// <returns>The cached HTTP client.</returns>
    ValueTask<HttpClient> Get(CancellationToken cancellationToken = default);
}
