using MediatR;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Interfaces;
using Products.APPLICATION.Mapping;
using Products.APPLICATION.Services;
using Products.DOMAIN.Entities;

namespace Products.APPLICATION.Feature.Query;

public record GetProductById(int Id)
    : IRequest<Result<Product>>
{
    public class GetProductByIdHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetProductById, Result<Product>>
    {
        public async Task<Result<Product>> Handle(
            GetProductById request,
            CancellationToken cancellationToken)
        {
            var product =
                await unitOfWork.productRepository
                    .GetByIdAsync(request.Id);


            return product == null
                 ? new Result<Product> { IsSucceeded = false, Error = "Product not found" }
                 : new Result<Product> { IsSucceeded = true, Data = product };
        }
    }
}