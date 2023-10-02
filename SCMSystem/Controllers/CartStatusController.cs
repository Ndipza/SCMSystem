using Core.Constants;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartStatusController : ControllerBase
    {
        #region Constructor

        private readonly ICartStatusService _cartStatusServices;
        private readonly ILogger<CartStatusController> _logger;
        public CartStatusController(ICartStatusService cartStatusServices, ILogger<CartStatusController> logger)
        {
            _cartStatusServices = cartStatusServices;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<CartStatusController>
        [HttpPost]
        [Route("CreateCartStatus")]
        public async Task<IActionResult> Post([FromBody] CartStatusViewModel cartStatusViewModel)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/cartstatus POST CartStatus");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new CartStatus Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _cartStatusServices.CreateCartStatusAsync(cartStatusViewModel);

                if (model == 0)
                {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New CartStatus entity not created");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Added new CartStatus entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create CartStatus Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<CartStatusController>
        [HttpGet]
        [Route("GetAllCartStatuss")]
        public async Task<IActionResult> GetAllCartStatuss(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cartstatus Get CartStatus: page = {page}");

                var model = await _cartStatusServices.GetAllCartStatuses();


                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartStatus on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartStatus on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var cartStatuses = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get CartStatus successfully");
                return Ok(cartStatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get CartStatuses Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/cartstatus to Get CartStatus by id : {id}");

                var model = await _cartStatusServices.GetCartStatusById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CartStatuses by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get CartStatus by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get CartStatus by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }

        }

        #endregion

        #region Update

        // PUT api/<CartStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartStatusViewModel cartStatusViewModel, int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/cartstatus Update CartStatus: id = {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a CartStatus Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _cartStatusServices.UpdateCartStatusAsync(cartStatusViewModel, id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated CartStatus by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated CartStatus: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update CartStatus by id {id} Error:  Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<CartStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/cartstatus Delete Category by id: {id}");

                var model = await _cartStatusServices.DeleteCartStatusById(id);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted CartStatuses: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete CartStatuses: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get CartStatuses by id Error: Error message = {ex.Message}, ex {JsonConvert.SerializeObject(ex)}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion

    }
}
