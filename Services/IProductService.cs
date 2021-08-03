using EFCoreServices.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreServices.Services
{
    public interface IProductService
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductByID(int ProdID);

        Task<ProductDto> Add(Product prod);

        Task<int> Delete(int ProdID);

        Task Update(Product prod);
    }
}
