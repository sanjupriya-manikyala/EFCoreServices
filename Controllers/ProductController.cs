using EFCoreServices.Models;
using EFCoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace EFCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        public ProductController(ProductService productService)
        {
            _productService = productService;
        }

        [HttpGet]
        [Route("Products")]
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
        [Route("Product/{id:int}")]
        public async Task<IActionResult> GetProductByID(int prodId)
        {
            try
            {
                var product = await _productService.GetProductByID(prodId);
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
        public async Task<IActionResult> Add([FromBody]Product prod)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var product_Id = await _productService.Add(prod);
                    if (product_Id != null)
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
            return BadRequest(ModelState);
        }

        
        [HttpDelete]
        [Route("RemoveProduct")]
        public async Task<IActionResult> Delete(int prodId)
        {
            int result;
            try
            {
                result = await _productService.Delete(prodId);
                if(result == 0)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch(Exception)
            {
                return BadRequest(ModelState);
            }
        }

        [HttpPut]
        [Route("UpdateProduct")]
        public async Task<IActionResult> Update([FromBody]Product prod)
        {
            if(ModelState.IsValid)
            {
                try
                {
                    await _productService.Update(prod);
                    return Ok();

                }
                catch(Exception ex)
                {
                    if(ex.GetType().FullName == "Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException")
                    {
                        return NotFound();
                    }
                    return BadRequest(ModelState);
                }
            }
            return BadRequest(ModelState);
        }

    }
}
