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
    public class CartItemTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CartItemTestServices()
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
        public async Task SaveAsync_AddNewCartItem()
        {
            /// Arrange
            var newCartItem = CartItemMockData.NewCartItem();
            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();

            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";

            var sut = new CartItemRepository(_context);

            /// Act
            await sut.CreateCartItem(newCartItem, userId);

            ///Assert
            int expectedRecordCount = (CartItemMockData.GetCartItems().Count() + 1);
            _context.CartItems.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCartItemCollection()
        {
            /// Arrange
            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";

            var sut = new CartItemRepository(_context);

            /// Act
            var result = await sut.GetAllCartItems(userId);

            /// Assert
            result.Should().HaveCount(CartItemMockData.GetCartItems().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCartItem()
        {
            /// Arrange
            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";

            var sut = new CartItemRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetCartItemById(id, userId);

            /// Assert
            result.Equals(CartItemMockData.GetCartItems().FirstOrDefault(x => x.Id == id));
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";
            _context.Carts.AddRange(CartMockData.GetCarts());

            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();

            var sut = new CartItemRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetCartItemById(id, userId);
            var okResult = existingPost.Should().BeOfType<Data.Models.CartItem>().Subject;
            var result = okResult.Quantity.Equals(CartItemMockData.GetCartItems().FirstOrDefault(x => x.Id == id).Quantity);
            okResult.Quantity.Equals(20);

            var CartItem = new CartItemViewModel();
            CartItem.Quantity = 30;

            var updatedData = await sut.UpdateCartItem(CartItem, id, userId);

            //Assert  
            result.Should().BeTrue();
            updatedData.Quantity.Equals(CartItem.Quantity);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            string? userId = "28f1a0af-71bc-4d9e-bc4e-eae210abbb79";
            _context.Carts.AddRange(CartMockData.GetCarts());

            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();

            var sut = new CartItemRepository(_context);

            //Act  
            var id = 100;
            var CartItem = new CartItemViewModel();
            CartItem.Quantity = 1000;

            var updatedData = await sut.UpdateCartItem(CartItem, id, userId);

            //Assert  
            Assert.Null(updatedData.Product);
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

            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();

            var sut = new CartItemRepository(_context);

            //Act
            var data = await sut.DeleteCartItem(id, userId);

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

            _context.CartItems.AddRange(CartItemMockData.GetCartItems());
            _context.SaveChanges();

            var sut = new CartItemRepository(_context);

            //Act
            var data = await sut.DeleteCartItem(id, userId);
            await sut.DeleteCartItem(id, userId);

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
