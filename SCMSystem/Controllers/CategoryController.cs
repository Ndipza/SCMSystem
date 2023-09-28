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
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetCategories()
        {
            try
            {
                var model = await _unitOfWork.CategoryRepository.GetCategories();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCategory(int id)
        {
            try
            {
                var model = await _unitOfWork.CategoryRepository.GetCategoryById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // POST api/<CategoryController>
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Post([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                var model = await _unitOfWork.CategoryRepository.CreateCategory(categoryViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CategoryViewModel categoryViewModel, int id)
        {
            try
            {
                var model = await _unitOfWork.CategoryRepository.UpdateCategory(categoryViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.CategoryRepository.DeleteCategory(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
