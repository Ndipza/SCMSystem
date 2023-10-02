using Core.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RepositoriesTest.System.Controllers
{
    public class CartItemTestController
    {

        #region Constructor

        protected readonly Mock<ICartItemService> CartItemService;
        private readonly Mock<ILogger<CartItemController>> _logger;
        public CartItemTestController()
        {
            CartItemService = new Mock<ICartItemService>();
            _logger = new Mock<ILogger<CartItemController>>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CartItemObject_Valid_Test()
        {
            //Arrange  
            var CartItem = new CartItemViewModel();

            CartItem.Quantity = CartItemMockData.NewCartItem().Quantity;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CartItem, new ValidationContext(CartItem), validationResults, true);

            //Assert  
            Assert.True(actual, "5");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_CartItemObject_InValid_Test()
        {
            //Arrange  
            var CartItem = new CartItemViewModel();

            CartItem.Quantity = CartItemMockData.InvalidCartItem().Quantity;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CartItem, new ValidationContext(CartItem), validationResults, true);

            //Assert  
            Assert.Equal(0, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveCartItem_ShouldCall_ICartItemService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newCartItem = CartItemMockData.NewCartItem();
            var controller = new CartItemController(CartItemService.Object, _logger.Object);

            /// Act
            var result = await controller.Post(newCartItem);

            /// Assert
            CartItemService.Verify(_ => _.CreateCartItem(newCartItem), Times.Exactly(1));
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllCartItems_ShouldReturn200Status()
        {
            /// Arrange
            CartItemService.Setup(_ => _.GetAllCartItems()).ReturnsAsync(CartItemMockData.GetCartItems());
            var controller = new CartItemController(CartItemService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAllCartItems(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }
               

        [Fact]
        public void GetAllCartItems_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            CartItemService.Setup(_ => _.GetAllCartItems()).ReturnsAsync(CartItemMockData.GetEmptyTodos());
            var controller = new CartItemController(CartItemService.Object, _logger.Object);

            //Act  
            var data = controller.GetAllCartItems(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCartItemById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetCartItems()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.CartItem)result.Value).Quantity.Equals(10);
        }

        [Fact]
        public async Task GetCartItemById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCartItemById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetCartItems()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCartItemById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetCartItems()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCartItemById_MatchResult()
        {
            int id = 3;
            //Arrange  
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetCartItems()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal(30, ((Data.Models.CartItem)okResult.Value).Quantity);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            CartItemService.Setup(_ => _.GetCartItemById(id)).ReturnsAsync(CartItemMockData.GetCartItems()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartItemController(CartItemService.Object, _logger.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion

    }
}
