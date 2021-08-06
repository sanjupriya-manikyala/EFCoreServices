using EFCoreServices.DTO;
using EFCoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace EFCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }


        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            try
            {
                var products = await _productService.GetProductsAsync();
                if (products == null)
                {
                    return NoContent();
                }
                return Ok(products);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetByIDAsync(int productId)
        {
            try
            {
                var product = await _productService.GetByIDAsync(productId);
                if(product == null)
                {
                    return NotFound();
                }
                return Ok(product);
            }
            catch(Exception ex)
            {
                return StatusCode(StatusCodes.Status503ServiceUnavailable, ex.Message);
            }
        }


        [HttpPost]
        public async Task<IActionResult> AddAsync([FromBody] ProductDto product)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product_Id = await _productService.AddAsync(product);
                    if (product_Id != null)
                    {
                        return Ok(product);
                    }
                    return NoContent();
                }
                catch (DbUpdateException)
                {
                    ModelState.AddModelError("", "Unable to save changes, please try again");
                }
            }
            return null;
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            try
            {
                int result = await _productService.DeleteAsync(productId);
                if(result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (DbUpdateException)
            {
                ModelState.AddModelError("", "Unable to save changes, please try again");
            }
            return null;
        }


        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] ProductDto product)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _productService.UpdateAsync(product);
                    return Ok();

                }
                catch(DbUpdateException)
                {
                    ModelState.AddModelError("","Unable to save changes, please try again");
                }
            }
            return null;
        }

    }
}
