using Core.Constants;
using Core.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Services.Interfaces;
using System.Security.Claims;

namespace SCMSystem.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : BaseController
    {

        #region Constructor

        private readonly ICartService _cartService;
        private readonly ILogger<CartController> _logger;
        public CartController(ICartService cartService, ILogger<CartController> logger)
        {
            _cartService = cartService;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<CartController>
        [HttpPost("{cartStatusId}")]
        public async Task<IActionResult> Post(int cartStatusId)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/cart POST");
                string? userId = GetUserId();

                if (userId == null) 
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found"); 
                }

                CartViewModel cartViewModel = new CartViewModel()
                {
                    CartStatusId = cartStatusId,
                    CustomerId = new Guid(userId)
                };                

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new Cart Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Create new Cart Error");
                }

                var model = await _cartService.CreateCart(cartViewModel);

                if (model == 0)
                {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New Cart entity not created");
                    return NotFound("New Cart entity not created");
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Added new Cart entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create Cart Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<CartController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cart Get Cart: page = {page}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _cartService.GetAllCarts();

                model = model.Where(x => x.CustomerId == new Guid(userId))?.ToList();

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Cart on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Cart on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var carts = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get Cart successfully");
                return Ok(carts);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get Cart Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cart to Get Cart by id : {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _cartService.GetCartById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Cart by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.CustomerId != new Guid(userId))
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"This cart {id} doesnt belong to customer {userId}");
                    return NotFound($"This cart {id} doesnt belong to customer {userId}");
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get Cart by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get Cart by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Update

        // PUT api/<CartController>/5
        [HttpPut("{id}/{cartStatusId}")]
        public async Task<IActionResult> Put(int id, int cartStatusId)
        {
            try
            {
                
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/cart Update Cart: id = {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a Cart Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                CartViewModel cartViewModel = new CartViewModel()
                {
                    CartStatusId = cartStatusId,
                    CustomerId = new Guid(userId)
                };

                var model = await _cartService.UpdateCart(cartViewModel, id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a Cart by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a Cart: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update Cart by id {id} Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/cart Delete Category by id: {id}");

                string? userId = GetUserId();

                if (userId == null)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Login user can't be found, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest("Login user can't be found");
                }

                var model = await _cartService.DeleteCart(id,userId);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted Cart: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete Cart: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get Cart by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion

    }
}
