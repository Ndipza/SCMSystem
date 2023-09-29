﻿using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        private readonly ICartItemService _CartItemService;
        public CartItemController(ICartItemService CartItemService)
        {
            _CartItemService = CartItemService;
        }

        // GET: api/<CartItemController>
        [HttpGet]
        [Route("GetAllCartItems")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _CartItemService.GetAllCartItems();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _CartItemService.GetCartItemById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }


        }

        // POST api/<CartItemController>
        [HttpPost]
        [Route("CreateCartItem")]
        public async Task<IActionResult> Post([FromBody] CartItemViewModel CartItemViewModel)
        {
            try
            {
                var model = await _CartItemService.CreateCartItem(CartItemViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<CartItemController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartItemViewModel CartItemViewModel, int id)
        {
            try
            {
                var model = await _CartItemService.UpdateCartItem(CartItemViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<CartItemController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _CartItemService.DeleteCartItem(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
