using EFCoreServices.Models;

namespace EFCoreServices.Test
{
    class DummyDataDBInitializer
    {
        public DummyDataDBInitializer()
        {
        }

        public void Seed(ProductDBContext context)
        {
            context.Database.EnsureDeleted();
            context.Database.EnsureCreated();

            context.Products.AddRange(
                new Product() { Id = 1, Name = "Test Name1", Price = 980 },
                new Product() { Id = 2, Name = "Test Name2", Price = 650 },
                new Product() { Id = 3, Name = "Test Name3", Price = 455 },
                new Product() { Id = 4, Name = "Test Name4", Price = 217 }
            );
            context.SaveChanges();
        }
    }
}
