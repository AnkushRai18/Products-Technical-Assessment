using FluentAssertions;
using Moq;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Feature.Command;
using Products.APPLICATION.Interfaces;
using Products.DOMAIN.Entities;
using Xunit;

namespace Products.Application.Tests.Commands;

public class UpdateProductTests
{
    [Fact]
    public async Task Should_Update_Product()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x =>
                x.GetByIdAsync(1))
            .ReturnsAsync(new Product
            {
                Id = 1,
                ProductName = "Old Product"
            });

        var unitOfWork =
            new Mock<IUnitOfWork>();

        unitOfWork.Setup(x =>
                x.productRepository)
            .Returns(repository.Object);

        unitOfWork.Setup(x =>
                x.SaveChangesAsync())
            .ReturnsAsync(1);

        var handler =
            new UpdateProduct.UpdateProductHandler(
                unitOfWork.Object);

        var result =
            await handler.Handle(
                new UpdateProduct(
                    new ProductDto
                    {
                        Id = 1,
                        ProductName = "New Product"
                    }),
                CancellationToken.None);

        result.IsSucceeded.Should().BeTrue();
    }
}