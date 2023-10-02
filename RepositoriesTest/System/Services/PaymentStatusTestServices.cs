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
    public class PaymentStatusTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public PaymentStatusTestServices()
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
        public async Task SaveAsync_AddNewPaymentStatus()
        {
            /// Arrange
            var newPaymentStatus = PaymentStatusMockData.NewPaymentStatus();
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            /// Act
            await sut.CreatePaymentStatusAsync(newPaymentStatus);

            ///Assert
            int expectedRecordCount = (PaymentStatusMockData.GetPaymentStatuses().Count() + 1);
            _context.PaymentStatuses.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnPaymentStatusCollection()
        {
            /// Arrange
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            /// Act
            var result = await sut.GetAllPaymentStatuses();

            /// Assert
            result.Should().HaveCount(PaymentStatusMockData.GetPaymentStatuses().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnPaymentStatus()
        {
            /// Arrange
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetPaymentStatusById(id);

            /// Assert
            result.Equals(PaymentStatusMockData.GetPaymentStatuses().FirstOrDefault(x => x.Id == id));
            result.Description.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetPaymentStatusById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.PaymentStatus>().Subject;
            var result = okResult.Description.Equals(PaymentStatusMockData.GetPaymentStatuses().FirstOrDefault(x => x.Id == id).Description);
            okResult.Description.Equals("Business and Finance", StringComparison.Ordinal);

            var PaymentStatus = new PaymentStatusViewModel();
            PaymentStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdatePaymentStatusAsync(PaymentStatus, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Description.Equals(PaymentStatus.Description, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            //Act  
            var id = 100;
            var PaymentStatus = new PaymentStatusViewModel();
            PaymentStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdatePaymentStatusAsync(PaymentStatus, id);

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
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            //Act
            var data = await sut.DeletePaymentStatusById(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.PaymentStatuses.AddRange(PaymentStatusMockData.GetPaymentStatuses());
            _context.SaveChanges();

            var sut = new PaymentStatusRepository(_context);

            //Act
            var data = await sut.DeletePaymentStatusById(id);
            await sut.DeletePaymentStatusById(id);

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
