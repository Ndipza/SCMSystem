using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IPaymentMethodService _paymentMethodService;
        public PaymentMethodController(IPaymentMethodService paymentMethodService)
        {
            _paymentMethodService = paymentMethodService;
        }
        // GET: api/<PaymentMethodController>
        [HttpGet]
        [Route("GetAllPaymentMethods")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _paymentMethodService.GetAllPaymentMethods();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _paymentMethodService.GetPaymentMethodById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }


        }

        // POST api/<PaymentMethodController>
        [HttpPost]
        [Route("CreatePaymentMethod")]
        public async Task<IActionResult> Post([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var model = await _paymentMethodService.CreatePaymentMethod(paymentMethodViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<PaymentMethodController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            try
            {
                var model = await _paymentMethodService.UpdatePaymentMethod(paymentMethodViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _paymentMethodService.DeletePaymentMethod(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
