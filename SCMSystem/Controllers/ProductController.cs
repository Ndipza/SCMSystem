using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public ProductController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<ProductController>
        [HttpGet]
        public Task<List<Product>> Get()
        {
            var model = _unitOfWork.ProductRepository.GetAll();
            return model;
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public Task<Product?> Get(int id)
        {
            var model = _unitOfWork.ProductRepository.GetById(id);
            return model;
        }

        // POST api/<ProductController>
        [HttpPost]
        public Task<long> Post([FromBody] ProductViewModel productViewModel)
        {
            var model = _unitOfWork.ProductRepository.InsertAsync(productViewModel);
            return model;
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public Task<Product> Put([FromBody] ProductViewModel productViewModel, int id)
        {
            var model = _unitOfWork.ProductRepository.UpdateAsync(productViewModel, id);
            return model;
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.ProductRepository.Delete(id);
        }
    }
}
