using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentMethodController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentMethodController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<PaymentMethodController>
        [HttpGet]
        [Route("GetAllPaymentMethods")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _unitOfWork.PaymentMethodRepository.GetAll();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _unitOfWork.PaymentMethodRepository.GetById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<PaymentMethodController>
        [HttpPost]
        [Route("CreateCategory")]
        public async Task<IActionResult> Post([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            try
            {
                var model = await _unitOfWork.PaymentMethodRepository.InsertAsync(paymentMethodViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<PaymentMethodController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            try
            {
                var model = await _unitOfWork.PaymentMethodRepository.UpdateAsync(paymentMethodViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.PaymentMethodRepository.Delete(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
