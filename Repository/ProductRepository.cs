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
            if(db != null)
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
            return null;
        }

        public async Task<int> Delete(int ProdID)
        {
            int result = 0;
            if(db != null)
            {
                var prod = await db.Products.SingleOrDefaultAsync(e => e.Id == ProdID);

                if(prod != null)
                {
                    db.Products.Remove(prod);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<ProductDto> GetProductByID(int ProdID)
        {
            if(db != null)
            {
                return await (from p in db.Products
                              select new ProductDto()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price
                              }).SingleOrDefaultAsync(p => p.Id == ProdID);
            }
            return null;
        }

        public async Task<List<ProductDto>> GetProductsAsync()
        {
            if(db != null)
            {
                return await (from p in db.Products
                              select new ProductDto()
                              {
                                  Id = p.Id,
                                  Name = p.Name,
                                  Price = p.Price
                              }).ToListAsync();
            }
            return null;
        }

        public async Task Update(Product prod)
        {
            if(db != null)
            {
                db.Products.Update(prod);
                await db.SaveChangesAsync();
            }
        }
    }
}
