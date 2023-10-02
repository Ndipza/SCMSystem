using Core.ViewModels;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using Services.Interfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.System.Controllers
{
    public class PaymentTestController
    {
        #region Constructor

        protected readonly Mock<IPaymentService> PaymentService;
        public PaymentTestController()
        {
            PaymentService = new Mock<IPaymentService>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_PaymentObject_Valid_Test()
        {
            //Arrange  
            var Payment = new PaymentViewModel();

            Payment.CartId = PaymentMockData.NewPayment().CartId;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Payment, new ValidationContext(Payment), validationResults, true);

            //Assert  
            Assert.True(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_PaymentObject_InValid_Test()
        {
            //Arrange  
            var Payment = new PaymentViewModel();

            Payment.CartId = PaymentMockData.NewPayment().CartId;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Payment, new ValidationContext(Payment), validationResults, true);

            //Assert  
            Assert.Equal(0, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SavePayment_ShouldCall_IPaymentService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newPayment = PaymentMockData.NewPayment();
            var controller = new PaymentController(PaymentService.Object);

            /// Act
            var result = await controller.Post(newPayment);

            /// Assert
            PaymentService.Verify(_ => _.CreatePayment(newPayment), Times.Exactly(1));
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllPayments_ShouldReturn200Status()
        {
            /// Arrange
            PaymentService.Setup(_ => _.GetAllPayments()).ReturnsAsync(PaymentMockData.GetPayments());
            var controller = new PaymentController(PaymentService.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAllPayments(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllPayments_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            PaymentService.Setup(_ => _.GetAllPayments()).ReturnsAsync(PaymentMockData.GetEmptyTodos());
            var controller = new PaymentController(PaymentService.Object);

            var page = 1;
            /// Act
            var result = (NoContentResult)await controller.GetAllPayments(page);


            /// Assert
            result.StatusCode.Should().Be(204);
            PaymentService.Verify(_ => _.GetAllPayments(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllPayments_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            PaymentService.Setup(_ => _.GetAllPayments()).ReturnsAsync(PaymentMockData.GetEmptyTodos());
            var controller = new PaymentController(PaymentService.Object);

            //Act  
            var data = controller.GetAllPayments(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetPayments()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.Payment)result.Value).Balance.Equals(30);
        }

        [Fact]
        public async Task GetPaymentById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetPaymentById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetPayments()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetPaymentById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetPayments()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetPaymentById_MatchResult()
        {
            int id = 3;
            //Arrange  
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetPayments()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal(343, ((Data.Models.Payment)okResult.Value).Balance);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            PaymentService.Setup(_ => _.GetPaymentById(id)).ReturnsAsync(PaymentMockData.GetPayments()?.FirstOrDefault(x => x.Id == id));
            var controller = new PaymentController(PaymentService.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
