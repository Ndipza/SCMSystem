using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productServicek;
        public ProductController(IProductService productServicek)
        {
            _productServicek = productServicek;
        }

        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(int page)
        {
            try
            {
                var model = await _productServicek.GetAllProducts();
                if (model == null) { return NotFound(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var products = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _productServicek.GetProductById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductViewModel productViewModel)
        {
            try
            {
                var model = await _productServicek.CreateProduct(productViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] ProductViewModel productViewModel, int id)
        {
            try
            {
                var model = await _productServicek.UpdateProduct(productViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            try
            {
                await _productServicek.DeleteProduct(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
