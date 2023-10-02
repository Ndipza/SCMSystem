using Core.ViewModels;
using Data.DTO;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using SCMSystem.Helper.Interface;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        #region Constructor

        private readonly IFileService _fileService;
        private readonly IProductService _productService;
        private readonly ILogger<ProductController> _logger;
        public ProductController(IProductService productService, ILogger<ProductController> logger, IFileService fileService)
        {
            _productService = productService;
            _logger = logger;
            this._fileService = fileService;
        }

        #endregion

        #region Create

        // POST api/<ProductController>
        [HttpPost]
        public async Task<IActionResult> Post([FromForm] ProductViewModel productViewModel)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (productViewModel.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(productViewModel.ImageFile);

                    if(fileResult == null) { return  NotFound(); }
                    if (fileResult.Item1 == 1)
                    {
                        productViewModel.ImageName = fileResult.Item2; // getting name of image
                    }
                    var model = await _productService.CreateProduct(productViewModel);

                    return Ok(model);
                }

                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAllProducts")]
        public async Task<IActionResult> GetAllProducts(int page)
        {
            try
            {
                var model = await _productService.GetAllProducts();
                if (model == null) { return NotFound(); }
                if (model.Count == 0) { return NoContent(); }

                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var products = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                return Ok(products);
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var model = await _productService.GetProductById(id);
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

        // PUT api/<ProductController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromForm] ProductViewModel productViewModel, int id)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }

                if (productViewModel.ImageFile != null)
                {
                    var fileResult = _fileService.SaveImage(productViewModel.ImageFile);
                    if (fileResult.Item1 == 1)
                    {
                        productViewModel.ImageName = fileResult.Item2; // getting name of image
                    }
                    var model = await _productService.UpdateProduct(productViewModel, id);
                    if (model == null) { return NotFound(); }

                    return Ok(model);
                }
                return BadRequest();
            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Delete

        // DELETE api/<ProductController>/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {

            try
            {

                var model = await _productService.DeleteProduct(id);

                if (model == null || !model.IsProductDeleted) { return NotFound(); }


                var delete = _fileService.DeleteImage(model.ImageName);
                return Ok(model);


            }
            catch (Exception ex)
            {
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion
    }
}
