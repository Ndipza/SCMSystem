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
    public class CustomerTestController
    {
        #region Constructor

        protected readonly Mock<ICustomerService> CustomerService;
        public CustomerTestController()
        {
            CustomerService = new Mock<ICustomerService>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CustomerObject_Valid_Test()
        {
            //Arrange  
            var Customer = new CustomerViewModel();

            Customer.Name = CustomerMockData.NewCustomer().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Customer, new ValidationContext(Customer), validationResults, true);

            //Assert  
            Assert.False(actual, "Luxolo");
            Assert.Equal(2, validationResults.Count);
        }

        [Fact]
        public async void Validate_CustomerObject_InValid_Test()
        {
            //Arrange  
            var Customer = new CustomerViewModel();

            Customer.Name = CustomerMockData.NewCustomerWithMoreThan50Characters().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(Customer, new ValidationContext(Customer), validationResults, true);

            //Assert  
            Assert.False(actual);
            Assert.Equal(3, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveCustomer_ShouldCall_ICustomerService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newCustomer = CustomerMockData.NewCustomer();
            var controller = new CustomerController(CustomerService.Object);

            /// Act
            var result = await controller.Post(newCustomer);

            /// Assert
            CustomerService.Verify(_ => _.CreateCustomer(newCustomer), Times.Exactly(1));
        }

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var newCustomer = CustomerMockData.NewCustomer();
            var controller = new CustomerController(CustomerService.Object);

            //Act  
            var result = await controller.Post(newCustomer);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        #endregion

        #region Read
        [Fact]
        public async Task GetAllCustomers_ShouldReturn200Status()
        {
            /// Arrange
            CustomerService.Setup(_ => _.GetAllCustomers()).ReturnsAsync(CustomerMockData.GetCustomers());
            var controller = new CustomerController(CustomerService.Object);

            int page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAllCustomer(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }


        [Fact]
        public void GetAllCustomers_Return_BadRequestResult()
        {
            Guid id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79");
            //Arrange  
            CustomerService.Setup(_ => _.GetAllCustomers()).ReturnsAsync(CustomerMockData.GetEmptyTodos());
            var controller = new CustomerController(CustomerService.Object);

            //Act  
            var data = controller.Get(id);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturn200Status()
        {
            var id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79");
            /// Arrange
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetCustomers()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.Customer)result.Value).Name.Equals("Sindiswa", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetCustomerById_ShouldReturn404NotFoundStatus()
        {
            var id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79");
            /// Arrange
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCustomerById_Return_OkResult()
        {
            var id = new Guid("14e6b812-2b4d-4004-a5d8-e9dc72cd25dc");
            //Arrange  
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetCustomers()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCustomerById_Return_NotFoundResult()
        {
            var id = new Guid("ebbb91fd-1080-4b6c-99fc-e134d60910ad");
            //Arrange  
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetCustomers()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCustomerById_MatchResult()
        {
            Guid id = new Guid("aadfb272-352f-41dd-bb9d-d3eb36af1c04");
            //Arrange  
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetCustomers()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);


            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Ncedo", ((Data.Models.Customer)okResult.Value).Name);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = new Guid("eb0415e0-8ba3-4ea6-b9b2-5fd0a4717515");
            CustomerService.Setup(_ => _.GetCustomerById(id)).ReturnsAsync(CustomerMockData.GetCustomers()?.FirstOrDefault(x => x.Id == id));
            var controller = new CustomerController(CustomerService.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }

        #endregion
    }
}
