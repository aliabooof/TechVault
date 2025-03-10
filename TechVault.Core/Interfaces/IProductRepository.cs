using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechVault.Core.Dto;
using TechVault.Core.Entities.Product;

namespace TechVault.Core.Interfaces
{
    public interface IProductRepository :IGenericRepository<Product>
    {
        Task<bool> AddAsync(AddProductDto addProductDto);
        Task<bool> UpdateAsync(UpdateProductDto updateProductDto);
        Task DeleteAsync(Product product);
    }

}
