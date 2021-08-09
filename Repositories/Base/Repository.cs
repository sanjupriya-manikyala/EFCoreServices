using EFCoreServices.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Linq;

namespace EFCoreServices.Repository
{
    public class Repository : IRepository<Product>
    {
        ProductDBContext db;
        public Repository(ProductDBContext _DbContext)
        {
            db = _DbContext;
        }

        public async Task<Product> AddAsync(Product product)
        {
            
                var result = await db.AddAsync(product);
                await db.SaveChangesAsync();
                return result.Entity;
        }

        public async Task DeleteAsync(int productID)
        {

                var product = await db.Products.SingleOrDefaultAsync(e => e.Id == productID);

                    if(product != null)
                    {
                        db.Products.Remove(product);
                        await db.SaveChangesAsync();
                    }
                
        }

        public async Task<Product> GetByIDAsync(int productID)
        {

            return await db.Products.Where(x => x.Id == productID).SingleOrDefaultAsync();

        }

        public async Task<List<Product>> GetProductsAsync()
        {

            return await db.Products.ToListAsync();

        }

        public async Task<Product> UpdateAsync(Product product)
        {

                var result = db.Products.Update(product);
                await db.SaveChangesAsync();
                return result.Entity;
        }
    }
}
