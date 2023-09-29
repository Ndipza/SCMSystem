using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartService _cartService;
        public CartController(ICartService cartService)
        {
            _cartService = cartService;
        }
        // GET: api/<CartController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _cartService.GetAllCarts();
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _cartService.GetCartById(id);
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // POST api/<CartController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CartViewModel cartViewModel)
        {            
            try
            {
                var model = await _cartService.CreateCart(cartViewModel);
                if (model == 0) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartViewModel cartViewModel, int id)
        {            
            try
            {
                var model = await _cartService.UpdateCart(cartViewModel, id);
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {           
            try
            {
                await _cartService.DeleteCart(id);

                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
