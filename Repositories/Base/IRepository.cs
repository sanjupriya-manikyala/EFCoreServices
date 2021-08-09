using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreServices.DTO;

namespace EFCoreServices.Repository
{
    public interface IRepository<Product>
    {
        Task<List<Product>> GetProductsAsync();

        Task<Product> GetByIDAsync(int productID);

        Task<Product> AddAsync(Product product);

        Task DeleteAsync(int productID);

        Task<Product> UpdateAsync(Product product);
    }
}
