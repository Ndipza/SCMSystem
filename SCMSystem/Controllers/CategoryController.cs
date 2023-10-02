using Core.Constants;
using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.Interfaces;
using System.Reflection;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        #region Constructor

        private readonly ICategoryService _categoryService;
        private readonly ILogger<CategoryController> _logger;

        public CategoryController(ICategoryService categoryService, ILogger<CategoryController> logger)
        {
            _categoryService = categoryService;
            _logger = logger;
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
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/category POST Category");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new Category Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }


                var model = await _categoryService.CreateCategory(categoryViewModel);

                if (model == 0) {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New Category entity not created");
                    return NotFound(); 
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Created new Category entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create Category Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
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
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/category Get Categories: page = {page}");

                var model = await _categoryService.GetCategories();

                if (model == null) {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Categories on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound(); 
                }

                if (model.Count == 0) {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Categories on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent(); 
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var categories = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get Categories successfully");
                return Ok(categories);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get Categories Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }

        }

        // GET api/<CategoryController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {

                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/category to Get category by id : {id}");

                var model = await _categoryService.GetCategoryById(id);

                
                if (model == null) {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Category by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound(); 
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get Category by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get Category by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
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
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/category Update Category: id = {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a Category Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _categoryService.UpdateCategory(categoryViewModel, id); 

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a Category by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a Category: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update a Category by id {id} Error:  Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
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
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/category Delete Category by id: {id}");

                var model = await _categoryService.DeleteCategory(id);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted Category: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete Category: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get Category by id Error: Error message = {ex.Message}, ex {JsonConvert.SerializeObject(ex)}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion

    }
}
