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
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CategoryController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        [Route("GetCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var model = _unitOfWork.CategoryRepository.GetCategories();
                if(model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
            
        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        [Route("GetCategory")]
        public Task<Category?> GetCategory(int id)
        {
            var model = _unitOfWork.CategoryRepository.GetById(id);
            return model;
        }

        // POST api/<CategoryController>
        [HttpPost]
        public Task<long> Post([FromBody] CategoryViewModel categoryViewModel)
        {
            var model = _unitOfWork.CategoryRepository.Create(categoryViewModel);
            return model;
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public Task<Category> Put([FromBody] CategoryViewModel categoryViewModel, int id)
        {
            var model = _unitOfWork.CategoryRepository.Update(categoryViewModel, id);
            return model;
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.CategoryRepository.Delete(id);
        }
    }
}
