using EFCoreServices.DTO;
using EFCoreServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace EFCoreServices.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly ProductService _productService;

        private readonly ILogger _logger;

        public ProductController(ProductService productService, ILogger logger)
        {
            _productService = productService;
            _logger = logger;

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
                _logger.LogInformation(StatusCodes.Status500InternalServerError, ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("{productId:int}")]
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
                _logger.LogInformation(StatusCodes.Status503ServiceUnavailable, ex.Message);
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
                catch (Exception ex)
                {
                    _logger.LogInformation(StatusCodes.Status422UnprocessableEntity, ex.Message);
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
                }
            }
            
        }


        [HttpDelete]
        public async Task<IActionResult> DeleteAsync(int productId)
        {
            try
            {
                await _productService.DeleteAsync(productId);
                return Ok();
            }
            catch (DbUpdateConcurrencyException ex)
            {
                _logger.LogInformation(StatusCodes.Status422UnprocessableEntity, ex.Message);
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
            }
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
                catch(Exception ex)
                {
                    _logger.LogInformation(StatusCodes.Status422UnprocessableEntity, ex.Message);
                    return StatusCode(StatusCodes.Status422UnprocessableEntity, ex.Message);
                }
            }
        }

    }
}
