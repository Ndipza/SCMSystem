using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        #endregion

        #region Create

        // POST api/<CategoryController>
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Post([FromBody] CategoryViewModel categoryViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _categoryService.CreateCategory(categoryViewModel);

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Read

        // GET: api/<CategoryController>
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(int page)
        {
            try
            {
                var model = await _categoryService.GetCategories();
                if (model == null) { return NotFound(); }
                if (model.Count == 0) { return NoContent(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var categories = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(categories);
            }
            catch (Exception)
            {
                return BadRequest();
            }

        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _categoryService.GetCategoryById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Update


        // PUT api/<CategoryController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CategoryViewModel categoryViewModel, int id)
        {
            try
            {
                var model = await _categoryService.UpdateCategory(categoryViewModel, id);
                if (model != null)
                {
                    return Ok(model);
                }

                return NotFound();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        #endregion

        #region Delete

        // DELETE api/<CategoryController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _categoryService.DeleteCategory(id);
                
                if(model)
                    return Ok(model);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion

    }
}
