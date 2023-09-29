﻿using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerStatusController : ControllerBase
    {
        private readonly ICustomerStatusService _customerStatusService;
        public CustomerStatusController(ICustomerStatusService customerStatusService)
        {
            _customerStatusService = customerStatusService;
        }

        // GET: api/<CustomerStatusController>
        [HttpGet]
        [Route("GetAllCustomerStatuss")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _customerStatusService.GetAllCustomerStatuses();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CustomerStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _customerStatusService.GetCustomerStatusById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }


        }

        // POST api/<CustomerStatusController>
        [HttpPost]
        [Route("CreateCustomerStatus")]
        public async Task<IActionResult> Post([FromBody] CustomerStatusViewModel customerStatusViewModel)
        {
            try
            {
                var model = await _customerStatusService.CreateCustomerStatus(customerStatusViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<CustomerStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CustomerStatusViewModel customerStatusViewModel, int id)
        {
            try
            {
                var model = await _customerStatusService.UpdateCustomerStatus(customerStatusViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<CustomerStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _customerStatusService.DeleteCustomerStatusById(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
