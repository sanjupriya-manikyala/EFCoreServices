using EFCoreServices.Controllers;
using EFCoreServices.DTO;
using EFCoreServices.Models;
using EFCoreServices.Services;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using Xunit;

namespace EFCoreServices.Test
{
    public class ProductUnitTest
    {

        private ProductService repository;
        public static DbContextOptions<ProductDBContext> dbContextOptions { get; }
        public static string ProductTestDBConnection;





        static ProductUnitTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ProductDBContext>()
                .UseSqlServer()
                .Options;
        }

        public ProductUnitTest()
        {
            var context = new ProductDBContext(dbContextOptions);
            DummyDataDBInitializer db = new DummyDataDBInitializer();
            db.Seed(context);

        }

        [Fact]
        public async void Task_GetProductsAsync_Return_OkResult()
        {
            //Arrange  
            var controller = new ProductController(repository);

            //Act  
            var data = await controller.GetProductsAsync();

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void Task_GetProductsAsync_Return_InternalServerError()
        {
            //Arrange  
            var controller = new ProductController(repository);
            controller.StatusCode(StatusCodes.Status500InternalServerError);

            //Act  
            var data = controller.GetProductsAsync();
            

            //Assert  
            var resultType = Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(StatusCodes.Status500InternalServerError, resultType.StatusCode);
        }

        [Fact]
        public async void Task_GetProductsAsync_MatchResult()
        {
            //Arrange  
            var controller = new ProductController(repository);

            //Act  
            var data = await controller.GetProductsAsync();

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeAssignableTo<List<Product>>().Subject;

            Assert.Equal(1, product[0].Id);
            Assert.Equal("Test Name1", product[0].Name);
            Assert.Equal(980, product[0].Price);

            Assert.Equal(2, product[1].Id);
            Assert.Equal("Test Name2", product[1].Name);
            Assert.Equal(650, product[1].Price);

            Assert.Equal(3, product[2].Id);
            Assert.Equal("Test Name3", product[2].Name);
            Assert.Equal(455, product[2].Price);

            Assert.Equal(4, product[3].Id);
            Assert.Equal("Test Name4", product[3].Name);
            Assert.Equal(217, product[3].Price);
        }


        [Fact]
        public async void Task_GetByIDAsync_Return_OkResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 2;

            //Act  
            var data = await controller.GetByIDAsync(productId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_GetByIDAsync_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 3;

            //Act  
            var data = await controller.GetByIDAsync(productId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_GetByIDAsync_Return_ServiceUnavailableError()
        {
            //Arrange  
            var controller = new ProductController(repository);
            int productId = 0;
            controller.StatusCode(StatusCodes.Status503ServiceUnavailable);
            //Act  
            var data = await controller.GetByIDAsync(productId);

            //Assert  
            var resultType = Assert.IsType<StatusCodeResult>(data);
            Assert.Equal(StatusCodes.Status503ServiceUnavailable, resultType.StatusCode);
        }

        [Fact]
        public async void Task_GetByIDAsync_MatchResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            int productId = 1;

            //Act  
            var data = await controller.GetByIDAsync(productId);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject;
            var product = okResult.Value.Should().BeAssignableTo<Product>().Subject;

            Assert.Equal(1, product.Id);
            Assert.Equal("Test Name1", product.Name);
            Assert.Equal(980, product.Price);
        }

        [Fact]
        public async void Task_AddAsync_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var product = new ProductDto() { Id = 5, Name = "Test Name5", Price = 8120};

            //Act  
            var data = await controller.AddAsync(product);

            //Assert  
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public async void Task_AddAsync_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var product = new ProductDto() { Id = 5, Name = "Test Name5", Price = 8120 };
            controller.ModelState.AddModelError("", "Unable to save changes, please try again");

            //Act              
            var data = await controller.AddAsync(product);

            //Assert  
            Assert.IsType<DbUpdateConcurrencyException>(data);
        }

        [Fact]
        public async void Task_AddAsync_ValidData_MatchResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var product = new ProductDto() { Id = 5, Name = "Test Name5", Price = 8120 };

            //Act  
            var data = await controller.AddAsync(product);

            //Assert  
            Assert.IsType<OkObjectResult>(data);

            var okResult = data.Should().BeOfType<OkObjectResult>().Subject; 

            Assert.Equal(3, okResult.Value);
        }

        [Fact]
        public async void Task_UpdateAsync_ValidData_Return_OkResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 2;

            //Act  
            var existingProduct = await controller.GetByIDAsync(productId);
            var okResult = existingProduct.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<ProductDto>().Subject;

            var product = new ProductDto();
            product.Id = 2;
            product.Name = result.Name;
            product.Price = result.Price;

            var updatedData = await controller.UpdateAsync(product);

            //Assert  
            Assert.IsType<OkResult>(updatedData);
        }

        [Fact]
        public async void Task_UpdateAsync_InvalidData_Return_BadRequest()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 2;
            controller.ModelState.AddModelError("", "Unable to save changes, please try again");

            //Act  
            var existingProduct = await controller.GetByIDAsync(productId);
            var okResult = existingProduct.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Product>().Subject;

            var product = new ProductDto();
            product.Id = 2;
            product.Name = result.Name;
            product.Price = result.Price;
            

            var data = await controller.UpdateAsync(product);

            //Assert  
            Assert.IsType<DbUpdateConcurrencyException>(data);
        }

        [Fact]
        public async void Task_UpdateAsync_InvalidData_Return_NotFound()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 2;

            //Act  
            var existingProduct = await controller.GetByIDAsync(productId);
            var okResult = existingProduct.Should().BeOfType<OkObjectResult>().Subject;
            var result = okResult.Value.Should().BeAssignableTo<Product>().Subject;

            var product = new ProductDto();
            product.Id = 2;
            product.Name = result.Name;
            product.Price = result.Price;

            var data = await controller.UpdateAsync(product);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_DeleteAsync_Return_OkResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 2;

            //Act  
            var data = await controller.DeleteAsync(productId);

            //Assert  
            Assert.IsType<OkResult>(data);
        }

        [Fact]
        public async void Task_DeleteAsync_Return_NotFoundResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            var productId = 4;

            //Act  
            var data = await controller.DeleteAsync(productId);

            //Assert  
            Assert.IsType<NotFoundResult>(data);
        }

        [Fact]
        public async void Task_DeleteAsync_Return_BadRequestResult()
        {
            //Arrange  
            var controller = new ProductController(repository);
            int productId = 3;
            controller.ModelState.AddModelError("", "Unable to save changes, please try again");

            //Act  
            var data = await controller.DeleteAsync(productId);

            //Assert  
            Assert.IsType<DbUpdateConcurrencyException>(data);
        }



    }
}
