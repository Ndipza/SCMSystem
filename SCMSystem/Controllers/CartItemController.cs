using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : ControllerBase
    {
        #region Constructor

        private readonly ICartItemService _CartItemService;
        public CartItemController(ICartItemService CartItemService)
        {
            _CartItemService = CartItemService;
        }

        #endregion

        #region Create

        // POST api/<CartItemController>
        [HttpPost]
        [Route("CreateCartItem")]
        public async Task<IActionResult> Post([FromBody] CartItemViewModel CartItemViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _CartItemService.CreateCartItem(CartItemViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<CartItemController>
        [HttpGet]
        [Route("GetAllCartItems")]
        public async Task<IActionResult> GetAllCartItems(int page)
        {
            try
            {
                var model = await _CartItemService.GetAllCartItems();
                if (model == null) { return NotFound(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var cartItems = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(cartItems);
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

        #endregion

        #region Update

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

        #endregion

        #region Delete

        // DELETE api/<CartItemController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {              
                var model = await _CartItemService.DeleteCartItem(id);

                if (model)
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
