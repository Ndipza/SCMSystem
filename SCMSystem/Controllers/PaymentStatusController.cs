using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentStatusController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentStatusController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<PaymentStatusController>
        [HttpGet]
        [Route("GetAllPaymentStatuss")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _unitOfWork.PaymentStatusRepository.GetAllPaymentStatuses();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PaymentStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _unitOfWork.PaymentStatusRepository.GetPaymentStatusById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<PaymentStatusController>
        [HttpPost]
        [Route("CreatePaymentStatus")]
        public async Task<IActionResult> Post([FromBody] PaymentStatusViewModel PaymentStatusViewModel)
        {
            try
            {
                var model = await _unitOfWork.PaymentStatusRepository.CreatePaymentStatusAsync(PaymentStatusViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PaymentStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentStatusViewModel PaymentStatusViewModel, int id)
        {
            try
            {
                var model = await _unitOfWork.PaymentStatusRepository.UpdatePaymentStatusAsync(PaymentStatusViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PaymentStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.PaymentStatusRepository.DeletePaymentStatusById(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
