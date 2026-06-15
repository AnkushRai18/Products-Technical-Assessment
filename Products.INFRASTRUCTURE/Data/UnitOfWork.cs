using Microsoft.EntityFrameworkCore;
using Products.APPLICATION.Interfaces;
using Products.INFRASTRUCTURE.Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.INFRASTRUCTURE.Data;

public class UnitOfWork(ApplicationDbContext context) : IUnitOfWork
{

    private IProductRepository? _productRepository;

    public IProductRepository productRepository =>
        _productRepository ??= new ProductRepository(context);

    public async Task<int> SaveChangesAsync()
    {
        return await context.SaveChangesAsync();
    }
}
