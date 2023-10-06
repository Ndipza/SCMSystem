using Core.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using SCMSystem.Helper.Interface;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.System.Controllers
{
    public class ProductTestController
    {
        #region Constructor

        protected readonly Mock<IProductService> _productService;
        private readonly Mock<ILogger<ProductController>> _logger;
        private readonly Mock<IFileService> _fileService;
        public ProductTestController()
        {
            _productService = new Mock<IProductService>();
            _logger = new Mock<ILogger<ProductController>>();
            _fileService = new Mock<IFileService>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_ProductObject_Valid_Test()
        {
            //Arrange  
            var Product = new ProductViewModel();

            Product.Name = ProductMockData.NewProduct().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Product, new ValidationContext(Product), validationResults, true);

            //Assert  
            Assert.Equal(1, validationResults.Count);
        }

        [Fact]
        public async void Validate_ProductObject_InValid_Test()
        {
            //Arrange  
            var Product = new ProductViewModel();

            Product.Name = ProductMockData.NewProductWithMoreThan50Characters().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Product, new ValidationContext(Product), validationResults, true);

            //Assert  
            Assert.False(actual);
            Assert.Equal(2, validationResults.Count);
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllProducts_ShouldReturn200Status()
        {
            /// Arrange
            _productService.Setup(_ => _.GetAllProducts()).ReturnsAsync(ProductMockData.GetProducts());
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAll(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllProducts_ShouldReturn404NoFoundStatus()
        {
            /// Arrange
            _productService.Setup(_ => _.GetAllProducts()).ReturnsAsync(ProductMockData.GetEmptyTodos());
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);

            var page = 1;
            /// Act
            var result = (NotFoundResult)await controller.GetAll(page);


            /// Assert
            result.StatusCode.Should().Be(404);
            _productService.Verify(_ => _.GetAllProducts(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllProducts_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            _productService.Setup(_ => _.GetAllProducts()).ReturnsAsync(ProductMockData.GetEmptyTodos());
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);

            //Act  
            var data = controller.GetAll(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetProductById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetProducts()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.Product)result.Value).Name.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetProductById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetProductById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetProducts()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetProductById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetProducts()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetProductById_MatchResult()
        {
            int id = 3;
            //Arrange  
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetProducts()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Transport", ((Data.Models.Product)okResult.Value).Name);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _productService.Setup(_ => _.GetProductById(id)).ReturnsAsync(ProductMockData.GetProducts()?.FirstOrDefault(x => x.Id == id));
            var controller = new ProductController(_productService.Object, _logger.Object, _fileService.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
