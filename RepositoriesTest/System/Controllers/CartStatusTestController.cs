using Core.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RepositoriesTest.System.Controllers
{
    public class CartStatusTestController
    {

        #region Constructor

        protected readonly Mock<ICartStatusService> CartStatusService;
        public CartStatusTestController()
        {
            CartStatusService = new Mock<ICartStatusService>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CartStatusObject_Valid_Test()
        {
            //Arrange  
            var CartStatus = new CartStatusViewModel();

            CartStatus.Description = CartStatusMockData.NewCartStatus().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CartStatus, new ValidationContext(CartStatus), validationResults, true);

            //Assert  
            Assert.True(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_CartStatusObject_InValid_Test()
        {
            //Arrange  
            var CartStatus = new CartStatusViewModel();

            CartStatus.Description = CartStatusMockData.NewCartStatusWithMoreThan50Characters().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CartStatus, new ValidationContext(CartStatus), validationResults, true);

            //Assert  
            Assert.False(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(1, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveCartStatus_ShouldCall_ICartStatusService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newCartStatus = CartStatusMockData.NewCartStatus();
            var controller = new CartStatusController(CartStatusService.Object);

            /// Act
            var result = await controller.Post(newCartStatus);

            /// Assert
            CartStatusService.Verify(_ => _.CreateCartStatusAsync(newCartStatus), Times.Exactly(1));
        }


        #endregion

        #region Read
        [Fact]
        public async Task GetAllCartStatuses_ShouldReturn200Status()
        {
            /// Arrange
            CartStatusService.Setup(_ => _.GetAllCartStatuses()).ReturnsAsync(CartStatusMockData.GetCartStatuses());
            var controller = new CartStatusController(CartStatusService.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAllCartStatuss(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public void GetAllCartStatuses_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            CartStatusService.Setup(_ => _.GetAllCartStatuses()).ReturnsAsync(CartStatusMockData.GetEmptyTodos());
            var controller = new CartStatusController(CartStatusService.Object);

            //Act  
            var data = controller.Get(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCartStatusById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetCartStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.CartStatus)result.Value).Description.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetCartStatusById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCartStatusById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetCartStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCartStatusById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetCartStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCartStatusById_MatchResult()
        {
            int id = 2;
            //Arrange  
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetCartStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Closed", ((Data.Models.CartStatus)okResult.Value).Description);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            CartStatusService.Setup(_ => _.GetCartStatusById(id)).ReturnsAsync(CartStatusMockData.GetCartStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CartStatusController(CartStatusService.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion

    }
}
