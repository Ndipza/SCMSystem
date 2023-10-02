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
    public class CartStatusTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CartStatusTestServices()
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
        public async Task SaveAsync_AddNewCartStatus()
        {
            /// Arrange
            var newCartStatus = CartStatusMockData.NewCartStatus();
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            /// Act
            await sut.CreateCartStatusAsync(newCartStatus);

            ///Assert
            int expectedRecordCount = (CartStatusMockData.GetCartStatuses().Count() + 1);
            _context.CartStatuses.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCartStatusCollection()
        {
            /// Arrange
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            /// Act
            var result = await sut.GetAllCartStatuses();

            /// Assert
            result.Should().HaveCount(CartStatusMockData.GetCartStatuses().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCartStatus()
        {
            /// Arrange
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetCartStatusById(id);

            /// Assert
            result.Equals(CartStatusMockData.GetCartStatuses().FirstOrDefault(x => x.Id == id));
            result.Description.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetCartStatusById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.CartStatus>().Subject;
            var result = okResult.Description.Equals(CartStatusMockData.GetCartStatuses().FirstOrDefault(x => x.Id == id).Description);
            okResult.Description.Equals("Closed", StringComparison.Ordinal);

            var CartStatus = new CartStatusViewModel();
            CartStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCartStatusAsync(CartStatus, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Description.Equals(CartStatus.Description, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            //Act  
            var id = 100;
            var CartStatus = new CartStatusViewModel();
            CartStatus.Description = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCartStatusAsync(CartStatus, id);

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
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            //Act
            var data = await sut.DeleteCartStatusById(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.CartStatuses.AddRange(CartStatusMockData.GetCartStatuses());
            _context.SaveChanges();

            var sut = new CartStatusRepository(_context);

            //Act
            var data = await sut.DeleteCartStatusById(id);
            await sut.DeleteCartStatusById(id);

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
