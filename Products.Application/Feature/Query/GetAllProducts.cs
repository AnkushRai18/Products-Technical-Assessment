using MediatR;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Interfaces;
using Products.APPLICATION.Mapping;
using Products.DOMAIN.Entities;

namespace Products.APPLICATION.Feature.Query;

public record GetAllProducts()
    : IRequest<List<Product>>
{
    public class GetAllProductsHandler(IUnitOfWork unitOfWork)
        : IRequestHandler<GetAllProducts, List<Product>>
    {
        public async Task<List<Product>> Handle(
            GetAllProducts request,
            CancellationToken cancellationToken)
        {
            var products =
                await unitOfWork.productRepository
                    .GetAllAsync();

            return products;
        }
    }
}