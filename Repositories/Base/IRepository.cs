using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreServices.DTO;

namespace EFCoreServices.Repository
{
    public interface IRepository<Product>
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetByIDAsync(int productID);

        Task AddAsync(Product product);

        Task<int> DeleteAsync(int productID);

        Task UpdateAsync(Product product);
    }
}
