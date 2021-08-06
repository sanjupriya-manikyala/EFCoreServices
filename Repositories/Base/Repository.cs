using EFCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;
using EFCoreServices.DTO;

namespace EFCoreServices.Repository
{
    public class Repository : IRepository<Product>
    {
        ProductDBContext db;
        public Repository(ProductDBContext _DbContext)
        {
            db = _DbContext;
        }

        public async Task AddAsync(Product product)
        {
            
                await db.AddAsync(product);
                await db.SaveChangesAsync();
        }

        public async Task<int> DeleteAsync(int productID)
        {

                var product = await db.Products.SingleOrDefaultAsync(e => e.Id == productID);

                    if(product != null)
                    {
                        db.Products.Remove(product);
                        await db.SaveChangesAsync();
                    }
                return 0;
        }

        public async Task<ProductDto> GetByIDAsync(int productID)
        {

                return await (from p in db.Products
                              select new ProductDto()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price
                              }).SingleOrDefaultAsync(p => p.Id == productID);

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

        public async Task UpdateAsync(Product product)
        {

                db.Products.Update(product);
                await db.SaveChangesAsync();
        }
    }
}
