using Core.ViewModels;

namespace RepositoriesTest.MockData
{
    public class CategoryMockData
    {
        public static List<Data.Models.Category> GetCategories()
        {
            return new List<Data.Models.Category>
            {
                 new Data.Models.Category{
                     Id = 1,
                     Name = "Automotive and Transport"
                 },
                 new Data.Models.Category{
                     Id = 2,
                     Name = "Business and Finance"
                 },
                 new Data.Models.Category{
                     Id = 3,
                     Name = "Chemicals and Materials"
                 },
                 new Data.Models.Category{
                     Id = 4,
                     Name = "Consumer Goods and Services"
                 },
                 new Data.Models.Category{
                     Id = 5,
                     Name = "Energy and Natural Resources"
                 },
                 new Data.Models.Category{
                     Id = 6,
                     Name = "Food and Beverage"
                 }
            };
        }

        public static List<Data.Models.Category> GetEmptyTodos()
        {
            return new List<Data.Models.Category>();
        }

        public static CategoryViewModel NewCategory()
        {
            return new CategoryViewModel
            {
                Name = "Healthcare"
            };
        }

        public static CategoryViewModel NewCategoryWithMoreThan50Characters()
        {
            return new CategoryViewModel
            {
                Name = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Name, which is not correct because in Category model, we have defined the size of the Name as 50 characters"
            };
        }
    }
}
