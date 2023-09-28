using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartStatusController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CartStatusController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CartStatusController>
        [HttpGet]
        [Route("GetAllCartStatuss")]
        public async Task<IActionResult> Get()
        {
            try
            {
                var model = await _unitOfWork.CartStatusRepository.GetAllCartStatuses();
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // GET api/<CartStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _unitOfWork.CartStatusRepository.GetCartStatusById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }


        }

        // POST api/<CartStatusController>
        [HttpPost]
        [Route("CreateCartStatus")]
        public async Task<IActionResult> Post([FromBody] CartStatusViewModel CartStatusViewModel)
        {
            try
            {
                var model = await _unitOfWork.CartStatusRepository.CreateCartStatusAsync(CartStatusViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // PUT api/<CartStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartStatusViewModel CartStatusViewModel, int id)
        {
            try
            {
                var model = await _unitOfWork.CartStatusRepository.UpdateCartStatusAsync(CartStatusViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE api/<CartStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _unitOfWork.CartStatusRepository.DeleteCartStatusById(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
