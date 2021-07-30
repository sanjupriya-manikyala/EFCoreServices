using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreServices.Models;

namespace EFCoreServices.Repository
{
    public interface IProductRepository
    {
        Task<List<Product>> GetProductsList();

        Task<Product> GetProductByID(int? ProdID);

        Task<int> AddProduct(Product prod);

        Task<int> DeleteProduct(int? ProdID);

        Task UpdateProduct(Product prod);
    }
}
