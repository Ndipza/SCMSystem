using Core.ViewModels;
using Data;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesTest.MockData;

namespace RepositoriesTest.System.Services
{
    public class CartTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CartTestServices()
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
        public async Task SaveAsync_AddNewCart()
        {
            /// Arrange
            var newCart = CartMockData.NewCart();
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            /// Act
            await sut.CreateCart(newCart);

            ///Assert
            int expectedRecordCount = (CartMockData.GetCarts().Count() + 1);
            _context.Carts.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCartCollection()
        {
            /// Arrange
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            /// Act
            var result = await sut.GetAllCarts();

            /// Assert
            result.Should().HaveCount(CartMockData.GetCarts().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCart()
        {
            /// Arrange
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetCartById(id);

            /// Assert
            result.Equals(CartMockData.GetCarts().FirstOrDefault(x => x.Id == id));
            result.CustomerId.Equals(new Guid("28f1a0af-71bc-4d9e-bc4e-eae210abbb79"));
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetCartById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.Cart>().Subject;
            var result = okResult.CustomerId.Equals(CartMockData.GetCarts().FirstOrDefault(x => x.Id == id).CustomerId);
            okResult.CustomerId.Equals(new Guid("14e6b812-2b4d-4004-a5d8-e9dc72cd25dc"));

            var Cart = new CartViewModel();
            Cart.CustomerId = new Guid("aadfb272-352f-41dd-bb9d-d3eb36af1c04");

            var updatedData = await sut.UpdateCart(Cart, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.CustomerId.Equals(Cart.CustomerId);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            //Act  
            var id = 100;
            var Cart = new CartViewModel();
            Cart.CustomerId = new Guid("aadfb272-352f-41dd-bb9d-d3eb36af1c04");

            var updatedData = await sut.UpdateCart(Cart, id);

            //Assert  
            Assert.Null(updatedData.CartStatus);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = 1;
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";

            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            //Act
            var data = await sut.DeleteCart(id, userId);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";
            _context.Carts.AddRange(CartMockData.GetCarts());
            _context.SaveChanges();

            var sut = new CartRepository(_context);

            //Act
            var data = await sut.DeleteCart(id, userId);
            await sut.DeleteCart(id, userId);

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
