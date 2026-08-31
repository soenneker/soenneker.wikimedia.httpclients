using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Soenneker.Dtos.HttpClientOptions;
using Soenneker.Extensions.Configuration;
using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Utils.HttpClientCache.Abstract;

namespace Soenneker.Wikimedia.HttpClients;

///<inheritdoc cref="IWikimediaOpenApiHttpClient"/>
public sealed class WikimediaOpenApiHttpClient : IWikimediaOpenApiHttpClient
{
    private readonly IHttpClientCache _httpClientCache;
    private readonly IConfiguration _config;
    private readonly string _cacheKey = $"{nameof(WikimediaOpenApiHttpClient)}-{Guid.NewGuid():N}";

    private const string _prodBaseUrl = "https://api.wikimedia.org/";

    public WikimediaOpenApiHttpClient(IHttpClientCache httpClientCache, IConfiguration config)
    {
        _httpClientCache = httpClientCache;
        _config = config;
    }

    public ValueTask<HttpClient> Get(CancellationToken cancellationToken = default)
    {
        return _httpClientCache.Get(_cacheKey, (config: _config, baseUrl: _config["Wikimedia:ClientBaseUrl"] ?? _prodBaseUrl), static state =>
        {
            string accessToken = state.config["Wikimedia:AccessToken"] ?? state.config.GetValueStrict<string>("Wikimedia:ApiKey");
            var userAgent = state.config.GetValueStrict<string>("Wikimedia:UserAgent");
            string authHeaderName = state.config["Wikimedia:AuthHeaderName"] ?? "Authorization";
            string authHeaderValueTemplate = state.config["Wikimedia:AuthHeaderValueTemplate"] ?? "Bearer {token}";
            string authHeaderValue = authHeaderValueTemplate.Replace("{token}", accessToken, StringComparison.Ordinal);

            return new HttpClientOptions
            {
                BaseAddress = new Uri(state.baseUrl),
                DefaultRequestHeaders = new Dictionary<string, string>
                {
                    {authHeaderName, authHeaderValue},
                    {"User-Agent", userAgent}
                }
            };
        }, cancellationToken);
    }

    public void Dispose()
    {
        _httpClientCache.RemoveSync(_cacheKey);
    }

    public ValueTask DisposeAsync()
    {
        return _httpClientCache.Remove(_cacheKey);
    }
}
