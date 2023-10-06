using Core.Constants;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Newtonsoft.Json;
using Services;
using Services.Interfaces;
using System.Reflection;

namespace SCMSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartItemController : BaseController
    {
        #region Constructor

        private readonly ICartItemService _CartItemService;
        private readonly ILogger<CartItemController> _logger;
        public CartItemController(ICartItemService CartItemService, ILogger<CartItemController> logger)
        {
            _CartItemService = CartItemService;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<CartItemController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CartItemViewModel CartItemViewModel)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/cartitem POST CartItem");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new CartItem Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _CartItemService.CreateCartItem(CartItemViewModel, userId);

                if (model == 0)
                {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New CartItem entity not created");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Added new CartItem entity with Id: {model}");
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
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cartitem Get Categories: page = {page}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _CartItemService.GetAllCartItems(userId);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartItems on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartItems on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var cartItems = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get CartItems successfully");
                return Ok(cartItems);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get CartItems Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartItemController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cartitems to Get category by id : {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _CartItemService.GetCartItemById(id, userId);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartItems by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get CartItems by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get CartItems by id Error: Error message = {ex.Message}");
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
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/cartitems Update Category: id = {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a CartItem Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _CartItemService.UpdateCartItem(CartItemViewModel, id, userId);


                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Updated a CartItems by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a CartItems: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update CartItems by id {id} Error: Error message = {ex.Message}");
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
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/cartitems Delete Category by id: {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _CartItemService.DeleteCartItem(id, userId);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted CartItems: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete CartItems: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get CartItems by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion

    }
}
