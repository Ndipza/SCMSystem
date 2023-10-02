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
    public class PaymentMethodTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public PaymentMethodTestServices()
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
        public async Task SaveAsync_AddNewPaymentMethod()
        {
            /// Arrange
            var newPaymentMethod = PaymentMethodMockData.NewPaymentMethod();
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            /// Act
            await sut.InsertAsync(newPaymentMethod);

            ///Assert
            int expectedRecordCount = (PaymentMethodMockData.GetPaymentMethods().Count() + 1);
            _context.PaymentMethods.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnPaymentMethodCollection()
        {
            /// Arrange
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            /// Act
            var result = await sut.GetAll();

            /// Assert
            result.Should().HaveCount(PaymentMethodMockData.GetPaymentMethods().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnPaymentMethod()
        {
            /// Arrange
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetById(id);

            /// Assert
            result.Equals(PaymentMethodMockData.GetPaymentMethods().FirstOrDefault(x => x.Id == id));
            result.Description.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.PaymentMethod>().Subject;
            var result = okResult.Description.Equals(PaymentMethodMockData.GetPaymentMethods().FirstOrDefault(x => x.Id == id).Description);
            okResult.Description.Equals("Business and Finance", StringComparison.Ordinal);

            var PaymentMethod = new PaymentMethodViewModel();
            PaymentMethod.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateAsync(PaymentMethod, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Description.Equals(PaymentMethod.Name, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            //Act  
            var id = 100;
            var PaymentMethod = new PaymentMethodViewModel();
            PaymentMethod.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateAsync(PaymentMethod, id);

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
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            //Act
            var data = await sut.Delete(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.PaymentMethods.AddRange(PaymentMethodMockData.GetPaymentMethods());
            _context.SaveChanges();

            var sut = new PaymentMethodRepository(_context);

            //Act
            var data = await sut.Delete(id);
            await sut.Delete(id);

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
