using FluentAssertions;
using Moq;
using Products.APPLICATION.Feature.Query;
using Products.APPLICATION.Interfaces;
using Products.DOMAIN.Entities;
using Xunit;

namespace Products.Application.Tests.Queries;

public class GetProductByIdTests
{
    [Fact]
    public async Task Should_Return_Product()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x =>
                x.GetByIdAsync(1))
            .ReturnsAsync(new Product
            {
                Id = 1,
                ProductName = "Laptop"
            });

        var unitOfWork =
            new Mock<IUnitOfWork>();

        unitOfWork.Setup(x =>
                x.productRepository)
            .Returns(repository.Object);

        var handler =
            new GetProductById.GetProductByIdHandler(
                unitOfWork.Object);

        var result =
            await handler.Handle(
                new GetProductById(1),
                CancellationToken.None);

        result.Should().NotBeNull();
    }
}