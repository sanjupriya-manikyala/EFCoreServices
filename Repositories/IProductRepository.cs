using EFCoreServices.DTO;
using EFCoreServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreServices.Repository
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetByIDAsync(int productID);

        Task<ProductDto> AddAsync(Product product);

        Task<int> DeleteAsync(int productID);

        Task<ProductDto> UpdateAsync(Product product);
    }
}
