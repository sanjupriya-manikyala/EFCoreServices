using EFCoreServices.Models;
using EFCoreServices.Repository;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EFCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        IProductRepository productRepository;
        public ProductController(ProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }

        [HttpGet]
        [Route("GetProductsList")]
        public async Task<IActionResult> GetProductsList()
        {
            try
            {
                var products = await productRepository.GetProductsList();
                if(products == null)
                {
                    return NotFound();
                }
                return Ok(products);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpGet]
        [Route("GetProductByID")]
        public async Task<IActionResult> GetProductByID(int? prodId)
        {
            try
            {
                var product = await productRepository.GetProductByID(prodId);
                if(product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost]
        [Route("AddProduct")]
        public async Task<IActionResult> AddProduct(Product prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product_Id = await productRepository.AddProduct(prod);
                    if (product_Id > 0)
                    {
                        return Ok(prod);
                    }
                    return NotFound();
                }
                catch (Exception)
                {
                    return BadRequest();
                }
            }
            return BadRequest();
        }

        
        [HttpDelete]
        [Route("RemoveProduct")]
        public async Task<IActionResult> DeleteProduct(int? prodId)
        {
            int result = 0;
            if(prodId == null)
            {
                return BadRequest();
            }
            try
            {
                result = await productRepository.DeleteProduct(prodId);
                if(result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest();
            }
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> UpdateProduct(Product prod)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await productRepository.UpdateProduct(prod);
                    return Ok();

                }
                catch(Exception ex)
                {
                    if(ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest();
                }
            }
            return BadRequest();
        }

    }
}
