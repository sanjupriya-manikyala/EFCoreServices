using EFCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EFCoreServices.Repository
{
    public class ProductRepository : IProductRepository
    {
        ProductDBContext db;
        public ProductRepository(ProductDBContext _DbContext)
        {
            db = _DbContext;
        }

        public async Task<int> AddProduct(Product prod)
        {
            if(db != null)
            {
                await db.Products.AddAsync(prod);
                await db.SaveChangesAsync();

            }
            return 0;
        }

        public async Task<int> DeleteProduct(int? ProdID)
        {
            int result = 0;
            if(db != null)
            {
                var prod = await db.Products.FirstOrDefaultAsync(e => e.Id == ProdID);

                if(prod != null)
                {
                    db.Products.Remove(prod);
                    result = await db.SaveChangesAsync();
                }
                return result;
            }
            return result;
        }

        public async Task<Product> GetProductByID(int? ProdID)
        {
            if(db != null)
            {
                return await db.Products.FirstOrDefaultAsync(e => e.Id == ProdID);
            }
            return null;
        }

        public async Task<List<Product>> GetProductsList()
        {
            if(db != null)
            {
                return await db.Products.ToListAsync();
            }
            return null;
        }

        public async Task UpdateProduct(Product prod)
        {
            if(db != null)
            {
                db.Products.Update(prod);
                await db.SaveChangesAsync();
            }
        }
    }
}
