using MediatR;
using Products.APPLICATION.Interfaces;
using Products.APPLICATION.Services;

namespace Products.APPLICATION.Feature.Command;

public record DeleteProduct(int Id) : IRequest<Result>
{
    public class DeleteProductHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<DeleteProduct, Result>
    {
        public async Task<Result> Handle(
            DeleteProduct request,
            CancellationToken cancellationToken)
        {
            var product =
                await unitOfWork.productRepository
                    .GetByIdAsync(request.Id);

            if (product == null)
                return Result.Failure("Product not found");

            unitOfWork.productRepository.Delete(product);

            int result =
                await unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success()
                : Result.Failure("Failed to delete product");
        }
    }
}