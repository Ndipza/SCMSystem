using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IPaymentStatusService _paymentStatusService;
        public PaymentStatusController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }
        // GET: api/<PaymentStatusController>
        [HttpGet]
        [Route("GetAllPaymentStatuss")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _paymentStatusService.GetAllPaymentStatuses();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<PaymentStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _paymentStatusService.GetPaymentStatusById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }


        }

        // POST api/<PaymentStatusController>
        [HttpPost]
        [Route("CreatePaymentStatus")]
        public async Task<IActionResult> Post([FromBody] PaymentStatusViewModel paymentStatusViewModel)
        {
            try
            {
                var model = await _paymentStatusService.CreatePaymentStatusAsync(paymentStatusViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<PaymentStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentStatusViewModel paymentStatusViewModel, int id)
        {
            try
            {
                var model = await _paymentStatusService.UpdatePaymentStatusAsync(paymentStatusViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<PaymentStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _paymentStatusService.DeletePaymentStatusById(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
