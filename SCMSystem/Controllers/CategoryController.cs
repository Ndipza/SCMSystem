﻿using Core.ViewModels;
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
        private readonly ICategoryService _categoryService;
        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }
        // GET: api/<CategoryController>
        [HttpGet]
        [Route("GetAllCategories")]
        public async Task<IActionResult> GetAllCategories(int page)
        {
            try
            {
                var model = await _categoryService.GetCategories();
                if (model == null) { return NotFound(); }

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
                var model = await _categoryService.UpdateCategory(categoryViewModel, id);
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
                await _categoryService.DeleteCategory(id);

                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }
    }
}
