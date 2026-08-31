[![](https://img.shields.io/nuget/v/soenneker.wikimedia.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.wikimedia.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.wikimedia.httpclients/publish-package.yml?style=for-the-badge)](https://github.com/soenneker/soenneker.wikimedia.httpclients/actions/workflows/publish-package.yml)
[![](https://img.shields.io/nuget/dt/soenneker.wikimedia.httpclients.svg?style=for-the-badge)](https://www.nuget.org/packages/soenneker.wikimedia.httpclients/)
[![](https://img.shields.io/github/actions/workflow/status/soenneker/soenneker.wikimedia.httpclients/codeql.yml?style=for-the-badge&label=codeql)](https://github.com/soenneker/soenneker.wikimedia.httpclients/actions/workflows/codeql.yml)

# ![](https://user-images.githubusercontent.com/4441470/224455560-91ed3ee7-f510-4041-a8d2-3fc093025112.png) Soenneker.Wikimedia.HttpClients

Provides a cached `HttpClient` configured with Wikimedia's API base address, bearer token, and required identifying user agent.

## Installation

```bash
dotnet add package Soenneker.Wikimedia.HttpClients
```

## Configuration

```json
{
  "Wikimedia": {
    "AccessToken": "your-access-token",
    "UserAgent": "MyApp/1.0 (https://example.com/contact)"
  }
}
```

`UserAgent` should identify your application and provide current contact information, as required by Wikimedia's [User-Agent policy](https://foundation.wikimedia.org/wiki/Policy:Wikimedia_Foundation_User-Agent_Policy/en).

`Wikimedia:ApiKey` remains supported as a legacy alias for `AccessToken`.

`Wikimedia:ClientBaseUrl` can override the default `https://api.wikimedia.org/` endpoint. `AuthHeaderName` and `AuthHeaderValueTemplate` are also available when a compatible endpoint uses a different authentication scheme; the value template may contain `{token}`.

## Registration and usage

```csharp
using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Wikimedia.HttpClients.Registrars;

services.AddWikimediaOpenApiHttpClientAsSingleton();

public sealed class WikimediaService
{
    private readonly IWikimediaOpenApiHttpClient _clientProvider;

    public WikimediaService(IWikimediaOpenApiHttpClient clientProvider)
    {
        _clientProvider = clientProvider;
    }

    public async Task<string> GetFileMetadata(CancellationToken cancellationToken)
    {
        HttpClient client = await _clientProvider.Get(cancellationToken);
        return await client.GetStringAsync(
            "core/v1/commons/file/File:The_Blue_Marble.jpg",
            cancellationToken);
    }
}
```

Use `AddWikimediaOpenApiHttpClientAsScoped()` when the provider should follow a scope. Each provider owns its cached client and removes it when disposed.
