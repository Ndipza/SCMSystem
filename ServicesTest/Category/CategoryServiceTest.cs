using AutoMapper;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Newtonsoft.Json.Linq;
using Services.Interfaces;
using SCMSystem.Controllers;

namespace ServicesTest.Category
{
    public class CategoryServiceTest
    {
        //private IMapper _mapper;

        private List<Data.Models.Category> _categories;
        private readonly IUnitOfWorkService _unitOfWork;

        public CategoryServiceTest()
        {
            _categories = new List<Data.Models.Category>();

            for (int i = 0; i < 10; i++)
            {
                var category = new Data.Models.Category
                {
                    Id = i,
                    Name = $"Test CategoryName{i}"
                };
                _categories.Add(category);
            }
        }
        //public CategoryServiceTest(IUnitOfWorkService unitOfWork)
        //{
        //    _unitOfWork = unitOfWork;
        //    _categories = new List<Data.Models.Category>();

        //    for (int i = 0; i < 10; i++)
        //    {
        //        var category = new Data.Models.Category
        //        {
        //            Id = i,
        //            Name = $"Test CategoryName{i}"
        //        };
        //        _categories.Add(category);
        //    }
        //}
        [Fact]
        public async void GetCategories()
        {
            //var mockCategory = CreateMockedCartegoryService();

            //var controller = new CategoryController(_unitOfWork);
            //var result = await controller.GetCategories();
            //Assert.NotNull(result);
            //Assert.True(result is OkObjectResult);

            //var responseResult = result as OkObjectResult;
            //Assert.True(responseResult.Value is List<Data.Models.Category>);

            //var responseValue = responseResult.Value as List<Data.Models.Category>;
            //Assert.NotNull(responseValue);
            //Assert.NotEmpty(responseValue);
            //Assert.True(Equal(_categories,responseValue));

        }

        private bool Equal(Data.Models.Category category, Data.Models.Category categoryDto)
        {
            return category.Id == categoryDto.Id && category.Name == categoryDto.Name;
        }

        private bool Equal(List<Data.Models.Category> categories, List<Data.Models.Category> categoriesDto)
        {
            if(categories.Count != categoriesDto.Count) return false;

            for (int i = 0; i < categories.Count; i++)
            {
                if (!Equal(categories[i], categoriesDto[i])) return false;
            }
            return true;
        }
        private ICategoryService CreateMockedCartegoryService()
        {
            var mockCategoryRepo = new Mock<ICategoryService>();
            mockCategoryRepo.Setup(service => service.GetCategories()).ReturnsAsync(_categories);

            foreach (var category in _categories)
            {
                mockCategoryRepo.Setup(service => service.GetCategoryById(category.Id)).ReturnsAsync(category);
            }

            var categoryViewModel = new CategoryViewModel
            {
                Name = $"Test CategoryName"
            };

            //mockCategoryRepo.Setup(service => service.Create(categoryViewModel)).ReturnsAsync(  );
            mockCategoryRepo.Setup(service => service.DeleteCategory(It.IsAny<Data.Models.Category>().Id));
            mockCategoryRepo.Setup(service => service.UpdateCategory(It.IsAny<CategoryViewModel>(), It.IsAny<Data.Models.Category>().Id)).ReturnsAsync((Data.Models.Category category) => category);

            return mockCategoryRepo.Object;
        }
    }
}
