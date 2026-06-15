using FluentAssertions;
using Moq;
using Products.APPLICATION.Feature.Command;
using Products.APPLICATION.Interfaces;
using Products.DOMAIN.Entities;
using Xunit;

namespace Products.Application.Tests.Commands;

public class DeleteProductTests
{
    [Fact]
    public async Task Should_Delete_Product()
    {
        var repository =
            new Mock<IProductRepository>();

        repository.Setup(x =>
                x.GetByIdAsync(1))
            .ReturnsAsync(new Product
            {
                Id = 1
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
            new DeleteProduct.DeleteProductHandler(
                unitOfWork.Object);

        var result =
            await handler.Handle(
                new DeleteProduct(1),
                CancellationToken.None);

        result.IsSucceeded.Should().BeTrue();
    }
}