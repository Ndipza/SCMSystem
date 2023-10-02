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
        #region Constructor

        private readonly IPaymentStatusService _paymentStatusService;
        public PaymentStatusController(IPaymentStatusService paymentStatusService)
        {
            _paymentStatusService = paymentStatusService;
        }

        #endregion

        #region Create

        // POST api/<PaymentStatusController>
        [HttpPost]
        [Route("CreatePaymentStatus")]
        public async Task<IActionResult> Post([FromBody] PaymentStatusViewModel paymentStatusViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _paymentStatusService.CreatePaymentStatusAsync(paymentStatusViewModel);
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

        // GET: api/<PaymentStatusController>
        [HttpGet]
        [Route("GetAllPaymentStatuss")]
        public async Task<IActionResult> GetAllPaymentStatuss(int page)
        {
            try
            {
                var model = await _paymentStatusService.GetAllPaymentStatuses();
                if (model == null) { return NotFound(); }
                if (model.Count == 0) { return NoContent(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var paymentStatuses = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(paymentStatuses);
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

        #endregion

        #region Update

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

        #endregion

        #region Delete

        // DELETE api/<PaymentStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _paymentStatusService.DeletePaymentStatusById(id);

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
