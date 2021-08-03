using EFCoreServices.Models;
using EFCoreServices.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreServices.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepo;

        public ProductService(IProductRepository productRepo)
        {
            _productRepo = productRepo;
        }

        public Task<List<ProductDto>> GetProductsAsync() => _productRepo.GetProductsAsync();

        public Task<ProductDto> GetProductByID(int prodID) => _productRepo.GetProductByID(prodID);

        public Task<ProductDto> Add(Product prod) => _productRepo.Add(prod);

        public Task<int> Delete(int prodID) => _productRepo.Delete(prodID);

        public Task Update(Product prod) => _productRepo.Update(prod);
    }

}
