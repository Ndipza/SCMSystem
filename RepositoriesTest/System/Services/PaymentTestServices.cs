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
    public class PaymentTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public PaymentTestServices()
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
        public async Task SaveAsync_AddNewPayment()
        {
            /// Arrange
            var newPayment = PaymentMockData.NewPayment();
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            /// Act
            await sut.CreatePayment(newPayment);

            ///Assert
            int expectedRecordCount = (PaymentMockData.GetPayments().Count() + 1);
            _context.Payments.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnPaymentCollection()
        {
            /// Arrange
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            /// Act
            var result = await sut.GetAllPayments();

            /// Assert
            result.Should().HaveCount(PaymentMockData.GetPayments().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnPayment()
        {
            /// Arrange
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetPaymentById(id);

            /// Assert
            result.Equals(PaymentMockData.GetPayments().FirstOrDefault(x => x.Id == id));
            result.CartId.Equals(3);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetPaymentById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.Payment>().Subject;
            var result = okResult.CartId.Equals(PaymentMockData.GetPayments().FirstOrDefault(x => x.Id == id).CartId);
            okResult.CartId.Equals(4);

            var Payment = new PaymentViewModel();
            Payment.CartId = 3;

            var updatedData = await sut.UpdatePayment(Payment, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.CartId.Equals(Payment.CartId);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            //Act  
            var id = 100;
            var Payment = new PaymentViewModel();
            Payment.CartId = 2;

            var updatedData = await sut.UpdatePayment(Payment, id);

            //Assert  
            Assert.Null(updatedData.PaymentStatus);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = 1;
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            //Act
            var data = await sut.DeletePayment(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.Payments.AddRange(PaymentMockData.GetPayments());
            _context.SaveChanges();

            var sut = new PaymentRepository(_context);

            //Act
            var data = await sut.DeletePayment(id);
            await sut.DeletePayment(id);

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
