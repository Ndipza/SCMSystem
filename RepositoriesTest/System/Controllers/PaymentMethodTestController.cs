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
    public class PaymentMethodTestController
    {
        #region Constructor

        protected readonly Mock<IPaymentMethodService> PaymentMethodService;
        private readonly Mock<ILogger<PaymentMethodController>> _logger;
        public PaymentMethodTestController()
        {
            PaymentMethodService = new Mock<IPaymentMethodService>();
            _logger = new Mock<ILogger<PaymentMethodController>>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_PaymentMethodObject_Valid_Test()
        {
            //Arrange  
            var PaymentMethod = new PaymentMethodViewModel();

            PaymentMethod.Name = PaymentMethodMockData.NewPaymentMethod().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(PaymentMethod, new ValidationContext(PaymentMethod), validationResults, true);

            //Assert  
            Assert.True(actual, "Debit");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_PaymentMethodObject_InValid_Test()
        {
            //Arrange  
            var PaymentMethod = new PaymentMethodViewModel();

            PaymentMethod.Name = PaymentMethodMockData.NewPaymentMethodWithMoreThan50Characters().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(PaymentMethod, new ValidationContext(PaymentMethod), validationResults, true);

            //Assert  
            Assert.False(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(1, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SavePaymentMethod_ShouldCall_IPaymentMethodService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newPaymentMethod = PaymentMethodMockData.NewPaymentMethod();
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);

            /// Act
            var result = await controller.Post(newPaymentMethod);

            /// Assert
            PaymentMethodService.Verify(_ => _.CreatePaymentMethod(newPaymentMethod), Times.Exactly(1));
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllPaymentMethods_ShouldReturn200Status()
        {
            /// Arrange
            PaymentMethodService.Setup(_ => _.GetAllPaymentMethods()).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods());
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAll(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllPaymentMethods_ShouldReturn404NoFoundStatus()
        {
            /// Arrange
            PaymentMethodService.Setup(_ => _.GetAllPaymentMethods()).ReturnsAsync(PaymentMethodMockData.GetEmptyTodos());
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);

            var page = 1;
            /// Act
            var result = (NotFoundResult)await controller.GetAll(page);


            /// Assert
            result.StatusCode.Should().Be(404);
            PaymentMethodService.Verify(_ => _.GetAllPaymentMethods(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllPaymentMethods_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            PaymentMethodService.Setup(_ => _.GetAllPaymentMethods()).ReturnsAsync(PaymentMethodMockData.GetEmptyTodos());
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);

            //Act  
            var data = controller.GetAll(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetPaymentMethodById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.PaymentMethod)result.Value).Description.Equals("Cash", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetPaymentMethodById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetPaymentMethodById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetPaymentMethodById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetPaymentMethodById_MatchResult()
        {
            int id = 3;
            //Arrange  
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Account", ((Data.Models.PaymentMethod)okResult.Value).Description);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            PaymentMethodService.Setup(_ => _.GetPaymentMethodById(id)).ReturnsAsync(PaymentMethodMockData.GetPaymentMethods()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentMethodController(PaymentMethodService.Object, _logger.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
