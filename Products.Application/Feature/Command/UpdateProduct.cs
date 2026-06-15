using MediatR;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Interfaces;
using Products.APPLICATION.Services;

namespace Products.APPLICATION.Feature.Command;

public record UpdateProduct(ProductDto Product) : IRequest<Result>
{
    public class UpdateProductHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<UpdateProduct, Result>
    {
        public async Task<Result> Handle(
            UpdateProduct request,
            CancellationToken cancellationToken)
        {
            var product =
                await unitOfWork.productRepository
                    .GetByIdAsync(request.Product.Id);

            if (product == null)
                return Result.Failure("Product not found");

            product.ProductName = request.Product.ProductName;
            product.ModifiedOn = DateTime.UtcNow;
            product.ModifiedBy = "Admin";

            unitOfWork.productRepository.Update(product);

            int result =
                await unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success()
                : Result.Failure("Failed to update product");
        }
    }
}