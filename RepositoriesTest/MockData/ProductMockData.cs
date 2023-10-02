using Core.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoriesTest.MockData
{
    public class ProductMockData
    {
        public static List<Data.Models.Product> GetProducts()
        {
            FileMock();
            return new List<Data.Models.Product>
            {
                 new Data.Models.Product{
                     Id = 1,
                     Name = "Automotive and Transport",
                     CategoryId = 1,
                     Image = "2b7d4c3e-d379-4306-8eb5-339c847e2bc1.jpg",
                     Price = 200
                 },
                 new Data.Models.Product{
                     Id = 2,
                     Name = "Automotive",
                     CategoryId = 1,
                     Image = "ec26013a-aa3a-4e4b-b6f9-f260b7be7c6a.jpg",
                     Price = 200
                 },
                 new Data.Models.Product{
                     Id = 3,
                     Name = "Transport",
                     CategoryId = 1,
                     Image = "b8c153ba-45e2-492a-98da-de079f10063d.jpg",
                     Price = 200
                 }
            };
        }

        private static IFormFile FileMock()
        {
            //Arrange
            var fileMock = new Mock<IFormFile>();
            //Setup mock file using a memory stream
            var content = "Hello World from a Fake File";
            var fileName = "test.pdf";
            var ms = new MemoryStream();
            var writer = new StreamWriter(ms);
            writer.Write(content);
            writer.Flush();
            ms.Position = 0;
            fileMock.Setup(_ => _.OpenReadStream()).Returns(ms);
            fileMock.Setup(_ => _.FileName).Returns(fileName);
            fileMock.Setup(_ => _.Length).Returns(ms.Length);

            return fileMock.Object;
        }

        public static List<Data.Models.Product> GetEmptyTodos()
        {
            return new List<Data.Models.Product>();
        }

        public static ProductViewModel NewProduct()
        {
            return new ProductViewModel
            {
                Name = "Healthcare",
                CategoryId = 1, 
                ImageName = "2b7d4c3e-d379-4306-8eb5-339c847e2bc1.jpg",
                ImageFile = FileMock()
            };
        }

        public static ProductViewModel NewProductWithMoreThan50Characters()
        {
            return new ProductViewModel
            {
                Name = "One thing you should notice here is that in \"Task_Add_InvalidData_Return_BadRequest\" Unit Test Cases, we are passing more than 50 characters for Name, which is not correct because in Product model, we have defined the size of the Name as 50 characters",
                CategoryId = 3,
                ImageName = "2b7d4c3e-d379-4306-8eb5-339c847e2bc1.jpg",
                ImageFile = FileMock()
            };
        }
    }
}
