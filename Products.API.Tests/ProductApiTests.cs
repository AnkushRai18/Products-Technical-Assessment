using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using System.Net;
using Xunit;

namespace Products.API.Tests;

public class ProductApiTests :
    IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _client;

    public ProductApiTests(
        WebApplicationFactory<Program> factory)
    {
        _client = factory.CreateClient();
    }

    [Fact]
    public async Task GetProducts_Should_Return_Ok()
    {
        var response =
            await _client.GetAsync(
                "/api/products");

        response.StatusCode
            .Should()
            .Be(HttpStatusCode.OK);
    }
}
