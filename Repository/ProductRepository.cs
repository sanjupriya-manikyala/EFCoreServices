using EFCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EFCoreServices.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDBContext db;
        public ProductRepository(ProductDBContext _DbContext)
        {
            db = _DbContext;
        }

        public async Task<ProductDto> Add(Product prod)
        {
            
                await db.AddAsync(prod);
                await db.SaveChangesAsync();

                var dto = new ProductDto()
                {
                    Id = prod.Id,
                    Name = prod.Name,
                    Price = prod.Price
                };

                return dto;
        }

        public async Task<int> Delete(int ProdID)
        {
            int result = 0;

                var prod = await db.Products.SingleOrDefaultAsync(e => e.Id == ProdID);

                if(prod != null)
                {
                    db.Products.Remove(prod);
                    result = await db.SaveChangesAsync();
                }
                return result;
        }

        public async Task<ProductDto> GetProductByID(int ProdID)
        {

                return await (from p in db.Products
                              select new ProductDto()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price
                              }).SingleOrDefaultAsync(p => p.Id == ProdID);

        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {

                return await (from p in db.Products
                              select new ProductDto()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price
                              }).ToListAsync();

        }

        public async Task Update(Product prod)
        {

                db.Products.Update(prod);
                await db.SaveChangesAsync(); 
        }
    }
}
