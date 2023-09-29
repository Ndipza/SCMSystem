using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualBasic;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }
        // GET: api/<PaymentController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            
            try
            {
                var model = await _paymentService.GetAllPayments();
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            
            try
            {
                var model = await _paymentService.GetPaymentById(id);
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentViewModel paymentViewModel)
        {            
            try
            {
                var model = await _paymentService.CreatePayment(paymentViewModel);
                if (model == 0) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentViewModel paymentViewModel, int id)
        {            
            try
            {
                var model = await _paymentService.UpdatePayment(paymentViewModel, id);
                if (model == null) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            
            try
            {
                await _paymentService.DeletePayment(id);


                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }

        }
    }
}
