using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Tests.HostedUnit;

namespace Soenneker.Wikimedia.HttpClients.Tests;

[ClassDataSource<Host>(Shared = SharedType.PerTestSession)]
public sealed class WikimediaOpenApiHttpClientTests : HostedUnitTest
{
    private readonly IWikimediaOpenApiHttpClient _httpclient;

    public WikimediaOpenApiHttpClientTests(Host host) : base(host)
    {
        _httpclient = Resolve<IWikimediaOpenApiHttpClient>(true);
    }

    [Test]
    public void Default()
    {

    }
}
