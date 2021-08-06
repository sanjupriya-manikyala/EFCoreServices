using EFCoreServices.DTO;
using EFCoreServices.Models;
using EFCoreServices.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreServices.Services
{
    public class ProductService
    {
        private readonly IRepository<Product> _repository;

        public ProductService(IRepository<Product> repository)
        {
            _repository = repository;
        }

        public Task<List<ProductDto>> GetProductsAsync() => _repository.GetProductsAsync();

        public Task<ProductDto> GetByIDAsync(int productID) => _repository.GetByIDAsync(productID);

        public async Task<ProductDto> AddAsync(ProductDto product)
        {
            var model = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };
            await _repository.AddAsync(model);
            return product;

        }

        public Task<int> DeleteAsync(int productID) => _repository.DeleteAsync(productID);

        public async Task<ProductDto> UpdateAsync(ProductDto product)
        {
            var model = new Product()
            {
                Id = product.Id,
                Name = product.Name,
                Price = product.Price
            };

            await _repository.UpdateAsync(model);
            return product;

        }
    }

}
