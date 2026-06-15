using MediatR;
using Products.APPLICATION.DTOs;
using Products.APPLICATION.Interfaces;
using Products.APPLICATION.Mapping;
using Products.APPLICATION.Services;
using Products.DOMAIN.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.Feature.Command;

public record AddProducts(CreateProductDto Product) : IRequest<Result>
{
    public class AddProductsHandler(IUnitOfWork unitOfWork) : IRequestHandler<AddProducts, Result>
    {
        public async Task<Result> Handle(AddProducts request, CancellationToken cancellationToken)
        {
            Product product = ObjectMapper.Map<Product>(request.Product);
            product.CreatedOn = DateTime.Now;

            await unitOfWork.productRepository.AddAsync(product);
            int result = await unitOfWork.SaveChangesAsync();

            return result > 0
                ? Result.Success()
                : Result.Failure("Failed to Add Products");

        }
    }
}
