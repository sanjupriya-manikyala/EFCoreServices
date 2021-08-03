﻿using System.Collections.Generic;
using System.Threading.Tasks;
using EFCoreServices.Models;

namespace EFCoreServices.Repository
{
    public interface IProductRepository
    {
        Task<List<ProductDto>> GetProductsAsync();

        Task<ProductDto> GetProductByID(int ProdID);

        Task<ProductDto> Add(Product prod);

        Task<int> Delete(int ProdID);

        Task Update(Product prod);
    }
}
