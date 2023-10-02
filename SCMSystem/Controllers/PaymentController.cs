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
        #region Constructor

        private readonly IPaymentService _paymentService;
        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }

        #endregion

        #region Create

        // POST api/<PaymentController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentViewModel paymentViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _paymentService.CreatePayment(paymentViewModel);
                if (model == 0) { return NotFound(); }


                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<PaymentController>
        [HttpGet]
        [Route("GetAllPayments")]
        public async Task<IActionResult> GetAllPayments(int page)
        {

            try
            {
                var model = await _paymentService.GetAllPayments();
                if (model == null) { return NotFound(); }
                if (model.Count == 0) { return NoContent(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var payments = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(payments);
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

        #endregion

        #region Update

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

        #endregion

        #region Delete

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {
                var model = await _paymentService.DeletePayment(id);

                if (model)
                    return Ok(model);

                return NotFound();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }

        }

        #endregion

    }
}
