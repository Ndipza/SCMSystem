using Core.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using Services;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartStatusController : ControllerBase
    {
        #region Constructor

        private readonly ICartStatusService _cartStatusServices;
        public CartStatusController(ICartStatusService cartStatusServices)
        {
            _cartStatusServices = cartStatusServices;
        }

        #endregion

        #region Create

        // POST api/<CartStatusController>
        [HttpPost]
        [Route("CreateCartStatus")]
        public async Task<IActionResult> Post([FromBody] CartStatusViewModel cartStatusViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                var model = await _cartStatusServices.CreateCartStatusAsync(cartStatusViewModel);
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

        // GET: api/<CartStatusController>
        [HttpGet]
        [Route("GetAllCartStatuss")]
        public async Task<IActionResult> GetAllCartStatuss(int page)
        {
            try
            {
                var model = await _cartStatusServices.GetAllCartStatuses();
                if (model == null) { return NotFound(); }
                if (model.Count == 0) { return NoContent(); }

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

        #endregion

        #region Update

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

        #endregion

        #region Delete

        // DELETE api/<CartStatusController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var model = await _cartStatusServices.DeleteCartStatusById(id);

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
