using Core.Constants;
using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Repositories.Interfaces;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        #region Constructor

        private readonly IPaymentMethodService _paymentMethodService;
        private readonly ILogger<PaymentMethodController> _logger;
        public PaymentMethodController(IPaymentMethodService paymentMethodService, ILogger<PaymentMethodController> logger)
        {
            _paymentMethodService = paymentMethodService;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<PaymentMethodController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/paymentmethod POST PaymentMethod");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new PaymentMethod Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _paymentMethodService.CreatePaymentMethod(paymentMethodViewModel);

                if (model == 0)
                {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New PaymentMethod entity not created");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Created new PaymentMethod entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create PaymentMethod Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<PaymentMethodController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/paymentmethod Get PaymentMethods: page = {page}");

                var model = await _paymentMethodService.GetAllPaymentMethods();

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get PaymentMethods on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get PaymentMethods on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var paymentMethods = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get PaymentMethods successfully");
                return Ok(paymentMethods);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get PaymentMethods Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/paymentmethod to Get PaymentMethod by id : {id}");

                var model = await _paymentMethodService.GetPaymentMethodById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get PaymentMethod by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get PaymentMethod by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get PaymentMethod by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Update

        // PUT api/<PaymentMethodController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/paymentmethod Update PaymentMethod: id = {id}");

                var model = await _paymentMethodService.UpdatePaymentMethod(paymentMethodViewModel, id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a PaymentMethod by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a PaymentMethod: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update a PaymentMethod by id {id} Error:  Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/paymentmethod Delete PaymentMethod by id: {id}");

                var model = await _paymentMethodService.DeletePaymentMethod(id);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted PaymentMethod: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete PaymentMethod: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get PaymentMethod by id Error: Error message = {ex.Message}, ex {JsonConvert.SerializeObject(ex)}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

    }
}
