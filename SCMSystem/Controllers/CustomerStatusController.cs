using Core.Constants;
using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerStatusController : ControllerBase
    {
        #region Constructor

        private readonly ICustomerStatusService _customerStatusService;
        private readonly ILogger<CustomerStatusController> _logger;
        public CustomerStatusController(ICustomerStatusService customerStatusService, ILogger<CustomerStatusController> logger)
        {
            _customerStatusService = customerStatusService;
            _logger = logger;
        }

        #endregion

        #region Create

        // POST api/<CustomerStatusController>
        [HttpPost]
        [Route("CreateCustomerStatus")]
        public async Task<IActionResult> Post([FromBody] CustomerStatusViewModel customerStatusViewModel)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/customerstatus POST CustomerStatus");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new CustomerStatus Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _customerStatusService.CreateCustomerStatus(customerStatusViewModel);

                if (model == 0)
                {
                    _logger.LogInformation(MyLogEvents.InsertItem, $"New CustomerStatus entity not created");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.InsertItem, $"Created new CustomerStatus entity with Id: {model}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create CustomerStatus Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<CustomerStatusController>
        [HttpGet]
        [Route("GetAllCustomerStatuss")]
        public async Task<IActionResult> GetAllCustomerStatuss(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/customerstatus Get CustomerStatuses: page = {page}");

                var model = await _customerStatusService.GetAllCustomerStatuses();

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CustomerStatuses on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CustomerStatuses on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var customerStatuses = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get CustomerStatuses successfully");
                return Ok(customerStatuses);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get CustomerStatuses Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CustomerStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/customerstatus to Get CustomerStatus by id : {id}");

                var model = await _customerStatusService.GetCustomerStatusById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get CustomerStatus by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get CustomerStatus by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get CustomerStatus by id Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Update

        // PUT api/<CustomerStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CustomerStatusViewModel customerStatusViewModel, int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/customerstatus Update CustomerStatus: id = {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a CustomerStatus Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
                    return BadRequest(ModelState);
                }

                var model = await _customerStatusService.UpdateCustomerStatus(customerStatusViewModel, id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a CustomerStatus by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a CustomerStatus: id = {model.Id}");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update a CustomerStatus by id {id} Error:  Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<CustomerStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/customerstatus Delete Category by id: {id}");

                var model = await _customerStatusService.DeleteCustomerStatusById(id);

                if (model)
                {
                    _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted CustomerStatus: id = {id}");
                    return Ok(model);
                }

                _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete CustomerStatus: id = {id}, NotFound : {NotFound().StatusCode}");
                return NotFound();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.DeleteItem, $"Get CustomerStatus by id Error: Error message = {ex.Message}, ex {JsonConvert.SerializeObject(ex)}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

    }
}
