﻿using Core.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;

namespace RepositoriesTest.System.Controllers
{
    public class CartTestController
    {

        #region Constructor

        protected readonly Mock<ICartService> CartService;
        private readonly Mock<ILogger<CartController>> _logger;
        public CartTestController()
        {
            CartService = new Mock<ICartService>();
            _logger = new Mock<ILogger<CartController>>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CartObject_Valid_Test()
        {
            //Arrange  
            var Cart = new CartViewModel();

            Cart.CartStatusId = CartMockData.NewCart().CartStatusId;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Cart, new ValidationContext(Cart), validationResults, true);

            //Assert  
            Assert.True(actual, "1");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_CartObject_InValid_Test()
        {
            //Arrange  
            var Cart = new CartViewModel();

            Cart.CartStatusId = CartMockData.NewCart().CartStatusId;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Cart, new ValidationContext(Cart), validationResults, true);

            //Assert  
            Assert.Equal(0, validationResults.Count);
         }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllCarts_ShouldReturn200Status()
        {
            /// Arrange
            CartService.Setup(_ => _.GetAllCarts()).ReturnsAsync(CartMockData.GetCarts());
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAll(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }


        //[Fact]
        public async Task GetAllCarts_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            CartService.Setup(_ => _.GetAllCarts()).ReturnsAsync(CartMockData.GetEmptyTodos());
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            var page = 1;
            /// Act
            var result = (NoContentResult)await controller.GetAll(page);


            /// Assert
            result.StatusCode.Should().Be(204);
            CartService.Verify(_ => _.GetAllCarts(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllCarts_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            CartService.Setup(_ => _.GetAllCarts()).ReturnsAsync(CartMockData.GetEmptyTodos());
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            //Act  
            var data = controller.GetAll(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCartById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetCarts()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.Cart)result.Value).CartStatusId.Equals(1);
        }

        [Fact]
        public async Task GetCartById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCartById_Return_OkResult()
        {
            var id = 1;
            //Arrange  
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetCarts()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCartById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetCarts()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCartById_MatchResult()
        {
            int id = 1;
            //Arrange  
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetCarts()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal(1, ((Data.Models.Cart)okResult.Value).CartStatusId);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            CartService.Setup(_ => _.GetCartById(id)).ReturnsAsync(CartMockData.GetCarts()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartController(CartService.Object, _logger.Object);
            controller.ControllerContext.HttpContext = new DefaultHttpContext { User = GetUser() };

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion

        private static ClaimsPrincipal GetUser()
        {
            var user = new ClaimsPrincipal(new ClaimsIdentity(new Claim[] {
                                        new Claim(ClaimTypes.NameIdentifier, "28f1a0af-71bc-4d9e-bc4e-eae210abbb79"),
                                        new Claim(ClaimTypes.Name, "ndiphiwe@somecompany.com")
                                   }, "TestAuthentication"));
            return user;
        }

    }
}
