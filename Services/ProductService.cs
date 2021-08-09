using EFCoreServices.DTO;
using EFCoreServices.Models;
using EFCoreServices.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EFCoreServices.Services
{
    public class ProductService
    {
        private readonly IProductRepository _repository;

        public ProductService(IProductRepository repository)
        {
            _repository = repository;
        }

        public Task<List<ProductDto>> GetProductsAsync() => _repository.GetProductsAsync();

        public Task<ProductDto> GetByIDAsync(int productID) => _repository.GetByIDAsync(productID);

        public async Task<ProductDto> AddAsync(ProductDto product)
        {
            var model = new Product()
            {
                Name = product.Name,
                Price = product.Price
            };
            var result = await _repository.AddAsync(model);
            return result;

        }

        public Task DeleteAsync(int productID) => _repository.DeleteAsync(productID);

        public async Task<ProductDto> UpdateAsync(ProductDto product)
        {
            var model = new Product();

            var result = await _repository.UpdateAsync(model);
            return result;

        }
    }

}
