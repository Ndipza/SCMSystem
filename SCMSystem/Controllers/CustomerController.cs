using Core.Constants;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        #region Constructor

        private readonly ICustomerService _customerService;
        private readonly ILogger<CustomerController> _logger;
        public CustomerController(ICustomerService customerService, ILogger<CustomerController> logger)
        {
            _customerService = customerService;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<CustomerController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CustomerViewModel customerViewModel)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/customer POST Customer");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new Customer Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _customerService.CreateCustomer(customerViewModel);

                _logger.LogInformation(MyLogEvents.InsertItem, $"Created new Customer entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create Customer Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<CustomerController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/customer Get Customers: page = {page}");

                var model = await _customerService.GetAllCustomers();

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Customers on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Customers on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var customers = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get Customers successfully");
                return Ok(customers);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get Customers Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/customers to Get Customers by id : {id}");

                var model = await _customerService.GetCustomerById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Customers by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get Customers by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get Customers by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Update

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CustomerViewModel customerViewModel, Guid id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/customers Update Customer: id = {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a Customer Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _customerService.UpdateCustomer(customerViewModel, id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a Customer by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a Customer: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update a Customer by id {id} Error:  Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {

            try
            {
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/customers Delete Customers by id: {id}");

                var model = await _customerService.DeleteCustomer(id);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted Customers: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete Customers: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get Customers by id Error: Error message = {ex.Message}, ex {JsonConvert.SerializeObject(ex)}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }
        #endregion
    }
}
