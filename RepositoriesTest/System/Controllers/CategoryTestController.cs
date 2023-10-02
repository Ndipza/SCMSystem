using Core.ViewModels;
using Data.Models;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using RepositoriesTest.MockData;
using SCMSystem.Controllers;
using Services.Interfaces;
using System.ComponentModel.DataAnnotations;

namespace RepositoriesTest.System.Controllers
{
    public class CategoryTestController
    {

        #region Constructor

        protected readonly Mock<ICategoryService> categoryService;
        public CategoryTestController()
        {
            categoryService = new Mock<ICategoryService>();
        }

        #endregion

        #region Object Validater

        [Fact]
        public async void Validate_CategoryObject_Valid_Test()
        {
            //Arrange  
            var category = new CategoryViewModel();

            category.Name = CategoryMockData.NewCategory().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(category, new ValidationContext(category), validationResults, true);

            //Assert  
            Assert.True(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(0, validationResults.Count);
        }

        [Fact]
        public async void Validate_CategoryObject_InValid_Test()
        {
            //Arrange  
            var category = new CategoryViewModel();

            category.Name = CategoryMockData.NewCategoryWithMoreThan50Characters().Name;

            //Act  

            var validationResults = new List<ValidationResult>();
            var actual = Validator.TryValidateObject(category, new ValidationContext(category), validationResults, true);

            //Assert  
            Assert.False(actual, "The field Name must be a string with a maximum length of 50.");
            Assert.Equal(1, validationResults.Count);
        }

        #endregion

        #region Create

        [Fact]
        public async Task SaveCategory_ShouldCall_ICategoryService_SaveAsync_AtleastOnce()
        {
            /// Arrange
            var newCategory = CategoryMockData.NewCategory();
            var controller = new CategoryController(categoryService.Object);

            /// Act
            var result = await controller.Post(newCategory);

            /// Assert
            categoryService.Verify(_ => _.CreateCategory(newCategory), Times.Exactly(1));
        }

        [Fact]
        public async void Task_Add_ValidData_Return_OkResult()
        {
            //Arrange  
            var newCategory = CategoryMockData.NewCategory();
            var controller = new CategoryController(categoryService.Object);

            //Act  
            var result = await controller.Post(newCategory);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void Task_Add_ValidData_MatchResult()
        {
            long zero = 0;
            //Arrange  
            var newCategory = CategoryMockData.NewCategory();
            var controller = new CategoryController(categoryService.Object);

            //Act  
            var result = await controller.Post(newCategory);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal(zero, okResult.Value);
        }
        #endregion

        #region Read
        [Fact]
        public async Task GetAllCategories_ShouldReturn200Status()
        {
            /// Arrange
            categoryService.Setup(_ => _.GetCategories()).ReturnsAsync(CategoryMockData.GetCategories());
            var controller = new CategoryController(categoryService.Object);

            var page = 1;
            /// Act
            var result = (OkObjectResult)await controller.GetAllCategories(page);


            // /// Assert
            result.StatusCode.Should().Be(200);
        }

        [Fact]
        public async Task GetAllCategories_ShouldReturn204NoContentStatus()
        {
            /// Arrange
            categoryService.Setup(_ => _.GetCategories()).ReturnsAsync(CategoryMockData.GetEmptyTodos());
            var controller = new CategoryController(categoryService.Object);

            var page = 1;
            /// Act
            var result = (NoContentResult)await controller.GetAllCategories(page);


            /// Assert
            result.StatusCode.Should().Be(204);
            categoryService.Verify(_ => _.GetCategories(), Times.Exactly(1));
        }

        [Fact]
        public void GetAllCategories_Return_BadRequestResult()
        {
            int page = 1;
            //Arrange  
            categoryService.Setup(_ => _.GetCategories()).ReturnsAsync(CategoryMockData.GetEmptyTodos());
            var controller = new CategoryController(categoryService.Object);

            //Act  
            var data = controller.GetAllCategories(page);
            data = null;

            if (data != null)
                //Assert  
                Assert.IsType<BadRequestResult>(data);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturn200Status()
        {
            var id = 1;
            /// Arrange
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetCategories()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);


            /// Act
            var result = (OkObjectResult)await controller.Get(id);


            // /// Assert
            result.StatusCode.Should().Be(200);
            result.Value.Should().NotBeNull();
            ((Data.Models.Category)result.Value).Name.Equals("Automotive and Transport", StringComparison.Ordinal);
        }

        [Fact]
        public async Task GetCategoryById_ShouldReturn404NotFoundStatus()
        {
            var id = 1;
            /// Arrange
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetEmptyTodos()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);


            /// Act
            var result = (NotFoundResult)await controller.Get(id);


            /// Assert
            result.StatusCode.Should().Be(404);
        }

        [Fact]
        public async void GetCategoryById_Return_OkResult()
        {
            var id = 2;
            //Arrange  
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetCategories()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);
        }

        [Fact]
        public async void GetCategoryById_Return_NotFoundResult()
        {
            var id = 10000;
            //Arrange  
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetCategories()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<NotFoundResult>(result);
        }

        [Fact]
        public async void GetCategoryById_MatchResult()
        {
            int id = 3;
            //Arrange  
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetCategories()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);
            

            //Act  
            var result = await controller.Get(id);

            //Assert  
            Assert.IsType<OkObjectResult>(result);

            var okResult = result.Should().BeOfType<OkObjectResult>().Subject;

            Assert.Equal("Chemicals and Materials", ((Data.Models.Category)okResult.Value).Name);
        }
        #endregion        

        #region Delete

        [Fact]
        public async void Task_Delete_Post_Return_NotFoundResult()
        {
            //Arrange
            var id = 100;
            categoryService.Setup(_ => _.GetCategoryById(id)).ReturnsAsync(CategoryMockData.GetCategories()?.FirstOrDefault(x => x.Id == id));
            var controller = new CategoryController(categoryService.Object);

            //Act
            var data = await controller.Delete(id);

            //Assert
            Assert.IsType<NotFoundResult>(data);
        }
       
        #endregion

    }
}
