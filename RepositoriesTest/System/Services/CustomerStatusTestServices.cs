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
    public class CustomerStatusTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CustomerStatusTestServices()
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
        public async Task SaveAsync_AddNewCustomerStatus()
        {
            /// Arrange
            var newCustomerStatus = CustomerStatusMockData.NewCustomerStatus();
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            /// Act
            await sut.CreateCustomerStatusAsync(newCustomerStatus);

            ///Assert
            int expectedRecordCount = (CustomerStatusMockData.GetCustomerStatuses().Count() + 1);
            _context.CustomerStatuses.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCustomerStatusCollection()
        {
            /// Arrange
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            /// Act
            var result = await sut.GetAllCustomerStatuses();

            /// Assert
            result.Should().HaveCount(CustomerStatusMockData.GetCustomerStatuses().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCustomerStatus()
        {
            /// Arrange
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetCustomerStatusById(id);

            /// Assert
            result.Equals(CustomerStatusMockData.GetCustomerStatuses().FirstOrDefault(x => x.Id == id));
            result.Description.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetCustomerStatusById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.CustomerStatus>().Subject;
            var result = okResult.Description.Equals(CustomerStatusMockData.GetCustomerStatuses().FirstOrDefault(x => x.Id == id).Description);
            okResult.Description.Equals("Business and Finance", StringComparison.Ordinal);

            var CustomerStatus = new CustomerStatusViewModel();
            CustomerStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCustomerStatusAsync(CustomerStatus, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Description.Equals(CustomerStatus.Description, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            //Act  
            var id = 100;
            var CustomerStatus = new CustomerStatusViewModel();
            CustomerStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCustomerStatusAsync(CustomerStatus, id);

            //Assert  
            Assert.Null(updatedData.Description);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = 1;
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            //Act
            var data = await sut.DeleteCustomerStatusById(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.CustomerStatuses.AddRange(CustomerStatusMockData.GetCustomerStatuses());
            _context.SaveChanges();

            var sut = new CustomerStatusRepository(_context);

            //Act
            var data = await sut.DeleteCustomerStatusById(id);
            await sut.DeleteCustomerStatusById(id);

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
