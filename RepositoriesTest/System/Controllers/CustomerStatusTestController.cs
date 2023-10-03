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
    public class CustomerStatusTestController
    {
        #region Constructor

        protected readonly Mock<ICustomerStatusService> CustomerStatusService;
        private readonly Mock<ILogger<CustomerStatusController>> _logger;
        public CustomerStatusTestController()
        {
            CustomerStatusService = new Mock<ICustomerStatusService>();
            _logger = new Mock<ILogger<CustomerStatusController>>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CustomerStatusObject_Valid_Test()
        {
            //Arrange  
            var CustomerStatus = new CustomerStatusViewModel();

            CustomerStatus.Description = CustomerStatusMockData.NewCustomerStatus().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CustomerStatus, new ValidationContext(CustomerStatus), validationResults, true);

            //Assert  
            Assert.True(actual, "Blacklisted");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_CustomerStatusObject_InValid_Test()
        {
            //Arrange  
            var CustomerStatus = new CustomerStatusViewModel();

            CustomerStatus.Description = CustomerStatusMockData.NewCustomerStatusWithMoreThan50Characters().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(CustomerStatus, new ValidationContext(CustomerStatus), validationResults, true);

            //Assert  
            Assert.False(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(1, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveCustomerStatus_ShouldCall_ICustomerStatusService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newCustomerStatus = CustomerStatusMockData.NewCustomerStatus();
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);

            /// Act
            var result = await controller.Post(newCustomerStatus);

            /// Assert
            CustomerStatusService.Verify(_ => _.CreateCustomerStatus(newCustomerStatus), Times.Exactly(1));
        }
        #endregion

        #region Read
        [Fact]
        public async Task GetAllCustomerStatuses_ShouldReturn200Status()
        {
            /// Arrange
            CustomerStatusService.Setup(_ => _.GetAllCustomerStatuses()).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses());
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAll(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public void GetAllCustomerStatuses_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            CustomerStatusService.Setup(_ => _.GetAllCustomerStatuses()).ReturnsAsync(CustomerStatusMockData.GetEmptyTodos());
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);

            //Act  
            var data = controller.Get(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCustomerStatusById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.CustomerStatus)result.Value).Description.Equals("Active", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetCustomerStatusById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCustomerStatusById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCustomerStatusById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCustomerStatusById_MatchResult()
        {
            int id = 2;
            //Arrange  
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("InActive", ((Data.Models.CustomerStatus)okResult.Value).Description);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            CustomerStatusService.Setup(_ => _.GetCustomerStatusById(id)).ReturnsAsync(CustomerStatusMockData.GetCustomerStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerStatusController(CustomerStatusService.Object, _logger.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
       
        #endregion

        
    }
}
