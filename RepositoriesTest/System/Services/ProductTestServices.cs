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
    public class ProductTestServices : IDisposable
    {
        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public ProductTestServices()
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
        public async Task SaveAsync_AddNewProduct()
        {
            /// Arrange
            var newProduct = ProductMockData.NewProduct();
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            /// Act
            await sut.CreateProduct(newProduct);

            ///Assert
            int expectedRecordCount = (ProductMockData.GetProducts().Count() + 1);
            _context.Products.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnProductCollection()
        {
            /// Arrange
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            /// Act
            var result = await sut.GetAllProducts();

            /// Assert
            result.Should().HaveCount(ProductMockData.GetProducts().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnProduct()
        {
            /// Arrange
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetProductById(id);

            /// Assert
            result.Equals(ProductMockData.GetProducts().FirstOrDefault(x => x.Id == id));
            result.Name.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetProductById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.Product>().Subject;
            var result = okResult.Name.Equals(ProductMockData.GetProducts().FirstOrDefault(x => x.Id == id).Name);
            okResult.Name.Equals("Business and Finance", StringComparison.Ordinal);

            var Product = new ProductViewModel();
            Product.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateProduct(Product, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Name.Equals(Product.Name, StringComparison.Ordinal);
        }

        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            //Act  
            var id = 100;
            var Product = new ProductViewModel();
            Product.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateProduct(Product, id);

            //Assert  
            Assert.Null(updatedData.Category);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = 1;
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            //Act
            var data = await sut.DeleteProduct(id);

            //Assert
            Assert.True(data.IsProductDeleted);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.Products.AddRange(ProductMockData.GetProducts());
            _context.SaveChanges();

            var sut = new ProductRepository(_context);

            //Act
            var data = await sut.DeleteProduct(id);
            await sut.DeleteProduct(id);

            //Assert
            Assert.False(data.IsProductDeleted);
        }

        #endregion       

        public void Dispose()
        {
            _context.Database.EnsureDeleted();
            _context.Dispose();
        }
    }
}
