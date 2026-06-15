using FluentAssertions;
using Moq;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Feature.Command;
using Products.APPLICATION.Interfaces;
using Xunit;

namespace Products.Application.Tests.Commands;

public class AddProductTests
{
    [Fact]
    public async Task Should_Add_Product_Successfully()
    {
        var unitOfWork = new Mock<IUnitOfWork>();

        unitOfWork.Setup(x =>
                x.SaveChangesAsync())
            .ReturnsAsync(1);

        var command =
            new AddProducts(new CreateProductDto
            {
                ProductName = "Laptop"
            });

        var handler =
            new AddProducts.AddProductsHandler(
                unitOfWork.Object);

        var result =
            await handler.Handle(
                command,
                CancellationToken.None);

        result.IsSucceeded.Should().BeTrue();
    }
}