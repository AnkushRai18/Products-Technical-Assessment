using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Products.APPLICATION.Interfaces;

public interface IUnitOfWork
{
    IProductRepository productRepository { get; }

    Task<int> SaveChangesAsync();
}
