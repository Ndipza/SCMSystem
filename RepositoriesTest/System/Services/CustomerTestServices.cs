using Core.ViewModels;
using Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesTest.MockData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.System.Services
{
    public class CustomerTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CustomerTestServices()
        {
            var options = new DbContextOptionsBuilder<SCMSystemDBContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new SCMSystemDBContext(options);

            _context.Database.EnsureCreated();
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveAsync_AddNewCustomer()
        {
            /// Arrange
            var newCustomer = CustomerMockData.NewCustomer();
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            /// Act
            await sut.CreateCustomer(newCustomer);

            ///Assert
            int expectedRecordCount = (CustomerMockData.GetCustomers().Count() + 1);
            _context.Customers.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCustomerCollection()
        {
            /// Arrange
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            /// Act
            var result = await sut.GetAllCustomers();

            /// Assert
            result.Should().HaveCount(CustomerMockData.GetCustomers().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCustomer()
        {
            /// Arrange
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            /// Act
            Guid id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79");
            var result = await sut.GetCustomerById(id);

            /// Assert
            result.Equals(CustomerMockData.GetCustomers().FirstOrDefault(x => x.Id == id));
            result.Name.Equals("Sindiswa", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            //Act  
            var id = new Guid("14e6b812-2b4d-4004-a5d8-e9dc72cd25dc");
            var existingPost = await sut.GetCustomerById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.Customer>().Subject;
            var result = okResult.Name.Equals(CustomerMockData.GetCustomers().FirstOrDefault(x => x.Id == id).Name);
            okResult.Name.Equals("Namhla", StringComparison.Ordinal);

            var Customer = new CustomerViewModel();
            Customer.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCustomers(Customer, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Name.Equals(Customer.Name, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            //Act  
            var id = new Guid("7c9e9011-81ad-4ef6-81ac-c2c69a762a58");
            var Customer = new CustomerViewModel();
            Customer.Name = "Sethu";

            var updatedData = await sut.UpdateCustomers(Customer, id);

            //Assert  
            Assert.Null(updatedData.CustomerStatus);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79");
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            //Act
            var data = await sut.DeleteCustomer(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = new Guid("7c9e9011-81ad-4ef6-81ac-c2c69a762a58");
            _context.Customers.AddRange(CustomerMockData.GetCustomers());
            _context.SaveChanges();

            var sut = new CustomerRepository(_context);

            //Act
            var data = await sut.DeleteCustomer(id);
            await sut.DeleteCustomer(id);

            //Assert
            Assert.False(data);
        }

        #endregion       

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
