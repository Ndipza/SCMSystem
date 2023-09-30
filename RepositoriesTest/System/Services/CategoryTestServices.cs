using Core.ViewModels;
using Data;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Repositories;
using RepositoriesTest.MockData;
using Services;
using System.ComponentModel.DataAnnotations;

namespace RepositoriesTest.System.Services
{
    public class CategoryTestServices : IDisposable
    {

        #region Constructor

        protected readonly SCMSystemDBContext _context;
        public CategoryTestServices()
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
        public async Task SaveAsync_AddNewCategory()
        {
            /// Arrange
            var newCategory = CategoryMockData.NewCategory();
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            /// Act
            await sut.CreateCategory(newCategory);

            ///Assert
            int expectedRecordCount = (CategoryMockData.GetCategories().Count() + 1);
            _context.Categories.Count().Should().Be(expectedRecordCount);
        }

        #endregion

        #region Read

        [Fact]
        public async Task GetAllAsync_ReturnCategoryCollection()
        {
            /// Arrange
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            /// Act
            var result = await sut.GetCategories();

            /// Assert
            result.Should().HaveCount(CategoryMockData.GetCategories().Count);
        }

        [Fact]
        public async Task GetByIdAsync_ReturnCategory()
        {
            /// Arrange
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            /// Act
            int id = 1;
            var result = await sut.GetCategoryById(id);

            /// Assert
            result.Equals(CategoryMockData.GetCategories().FirstOrDefault(x => x.Id == id));
            result.Name.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        #endregion

        #region Update

        [Fact]
        public async void Update_ValidData_Return_CorrectResults()
        {
            //Arrange  
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            //Act  
            var id = 2;
            var existingPost = await sut.GetCategoryById(id);
            var okResult = existingPost.Should().BeOfType<Data.Models.Category>().Subject;
            var result = okResult.Name.Equals(CategoryMockData.GetCategories().FirstOrDefault(x => x.Id == id).Name);
            okResult.Name.Equals("Business and Finance", StringComparison.Ordinal);

            var category = new CategoryViewModel();
            category.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCategory(category, id);

            //Assert  
            result.Should().BeTrue();
            updatedData.Name.Equals(category.Name, StringComparison.Ordinal);            
        }
        
        [Fact]
        public async void Update_InvalidData_Return_Null()
        {
            //Arrange  
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            //Act  
            var id = 100;            
            var category = new CategoryViewModel();
            category.Name = "Test Title 2 Updated";

            var updatedData = await sut.UpdateCategory(category, id);

            //Assert  
            Assert.Null(updatedData);
        }

        #endregion

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_OkResult()
        {
            //Arrange
            var id = 1;
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            //Act
            var data = await sut.DeleteCategory(id);

            //Assert
            Assert.True(data);
        }

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            _context.Categories.AddRange(CategoryMockData.GetCategories());
            _context.SaveChanges();

            var sut = new CategoryRepository(_context);

            //Act
            var data = await sut.DeleteCategory(id);
            await sut.DeleteCategory(id);

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
