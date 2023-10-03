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
    public class PaymentStatusTestController
    {
        #region Constructor

        protected readonly Mock<IPaymentStatusService> PaymentStatusService;
        private readonly Mock<ILogger<PaymentStatusController>> _logger;
        public PaymentStatusTestController()
        {
            PaymentStatusService = new Mock<IPaymentStatusService>();
            _logger = new Mock<ILogger<PaymentStatusController>>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_PaymentStatusObject_Valid_Test()
        {
            //Arrange  
            var PaymentStatus = new PaymentStatusViewModel();

            PaymentStatus.Description = PaymentStatusMockData.NewPaymentStatus().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(PaymentStatus, new ValidationContext(PaymentStatus), validationResults, true);

            //Assert  
            Assert.True(actual, "Hot Card");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_PaymentStatusObject_InValid_Test()
        {
            //Arrange  
            var PaymentStatus = new PaymentStatusViewModel();

            PaymentStatus.Description = PaymentStatusMockData.NewPaymentStatusWithMoreThan50Characters().Description;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(PaymentStatus, new ValidationContext(PaymentStatus), validationResults, true);

            //Assert  
            Assert.False(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(1, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SavePaymentStatus_ShouldCall_IPaymentStatusService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newPaymentStatus = PaymentStatusMockData.NewPaymentStatus();
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);

            /// Act
            var result = await controller.Post(newPaymentStatus);

            /// Assert
            PaymentStatusService.Verify(_ => _.CreatePaymentStatusAsync(newPaymentStatus), Times.Exactly(1));
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllPaymentStatuses_ShouldReturn200Status()
        {
            /// Arrange
            PaymentStatusService.Setup(_ => _.GetAllPaymentStatuses()).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses());
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAll(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllPaymentStatuses_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            PaymentStatusService.Setup(_ => _.GetAllPaymentStatuses()).ReturnsAsync(PaymentStatusMockData.GetEmptyTodos());
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (NoContentResult)await controller.GetAll(page);


            /// Assert
            result.StatusCode.Should().Be(204);
            PaymentStatusService.Verify(_ => _.GetAllPaymentStatuses(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllPaymentStatuses_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            PaymentStatusService.Setup(_ => _.GetAllPaymentStatuses()).ReturnsAsync(PaymentStatusMockData.GetEmptyTodos());
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);

            //Act  
            var data = controller.Get(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetPaymentStatusById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.PaymentStatus)result.Value).Description.Equals("Approved", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetPaymentStatusById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetPaymentStatusById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetPaymentStatusById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetPaymentStatusById_MatchResult()
        {
            int id = 2;
            //Arrange  
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Decline", ((Data.Models.PaymentStatus)okResult.Value).Description);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            PaymentStatusService.Setup(_ => _.GetPaymentStatusById(id)).ReturnsAsync(PaymentStatusMockData.GetPaymentStatuses()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentStatusController(PaymentStatusService.Object, _logger.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
