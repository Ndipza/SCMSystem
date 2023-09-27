using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CategoryController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        public Task<List<Category>> Get()
        {
            var model = _unitOfWork.Category.GetAll();
            return model;
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public Task<Category?> Get(int id)
        {
            var model = _unitOfWork.Category.GetById(id);
            return model;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public Task<long> Post([FromBody] CategoryViewModel categoryViewModel)
        {
            var model = _unitOfWork.Category.InsertAsync(categoryViewModel);
            return model;
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public Task<Category> Put([FromBody] CategoryViewModel categoryViewModel, int id)
        {
            var model = _unitOfWork.Category.UpdateAsync(categoryViewModel, id);
            return model;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.Category.Delete(id);
        }
    }
}
