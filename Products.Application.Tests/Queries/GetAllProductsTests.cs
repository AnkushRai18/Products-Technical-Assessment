using FluentAssertions;
using Moq;
using Products.APPLICATION.Feature.Query;
using Products.APPLICATION.Interfaces;
using Products.DOMAIN.Entities;
using Xunit;

namespace Products.Application.Tests.Queries;

public class GetAllProductsTests
{
    [Fact]
    public async Task Should_Return_Products()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x =>
                x.GetAllAsync())
            .ReturnsAsync(new List<Product>
            {
                new()
                {
                    Id = 1,
                    ProductName = "Laptop"
                }
            });

        var unitOfWork =
            new Mock<IUnitOfWork>();

        unitOfWork.Setup(x =>
                x.productRepository)
            .Returns(repository.Object);

        var handler =
            new GetAllProducts.GetAllProductsHandler(
                unitOfWork.Object);

        var result =
            await handler.Handle(
                new GetAllProducts(),
                CancellationToken.None);

        result.Count.Should().Be(1);
    }
}