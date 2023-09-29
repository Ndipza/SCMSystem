using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartStatusController : ControllerBase
    {
        private readonly ICartStatusService _cartStatusServices;
        public CartStatusController(ICartStatusService cartStatusServices)
        {
            _cartStatusServices = cartStatusServices;
        }
        // GET: api/<CartStatusController>
        [HttpGet]
        [Route("GetAllCartStatuss")]
        public async Task<IActionResult> GetAllCartStatuss(int page)
        {
            try
            {
                var model = await _cartStatusServices.GetAllCartStatuses();
                if (model == null) { return NotFound(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var cartStatuses = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(cartStatuses);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<CartStatusController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _cartStatusServices.GetCartStatusById(id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }


        }

        // POST api/<CartStatusController>
        [HttpPost]
        [Route("CreateCartStatus")]
        public async Task<IActionResult> Post([FromBody] CartStatusViewModel cartStatusViewModel)
        {
            try
            {
                var model = await _cartStatusServices.CreateCartStatusAsync(cartStatusViewModel);
                if (model == 0) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // PUT api/<CartStatusController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] CartStatusViewModel cartStatusViewModel, int id)
        {
            try
            {
                var model = await _cartStatusServices.UpdateCartStatusAsync(cartStatusViewModel, id);
                if (model == null) { return NotFound(); }

                return Ok(model);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // DELETE api/<CartStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cartStatusServices.DeleteCartStatusById(id);

                return Ok($"{id} Deleted");
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }
    }
}
