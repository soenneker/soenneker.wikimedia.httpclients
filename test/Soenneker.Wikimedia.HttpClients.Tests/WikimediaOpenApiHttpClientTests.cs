using Soenneker.Wikimedia.HttpClients.Abstract;
using Soenneker.Tests.FixturedUnit;
using Xunit;

namespace Soenneker.Wikimedia.HttpClients.Tests;

[Collection("Collection")]
public sealed class WikimediaOpenApiHttpClientTests : FixturedUnitTest
{
    private readonly IWikimediaOpenApiHttpClient _httpclient;

    public WikimediaOpenApiHttpClientTests(Fixture fixture, ITestOutputHelper output) : base(fixture, output)
    {
        _httpclient = Resolve<IWikimediaOpenApiHttpClient>(true);
    }

    [Fact]
    public void Default()
    {

    }
}
