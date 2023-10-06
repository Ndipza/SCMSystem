using Core.Constants;
using Core.ViewModels;
using Data.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Web.Resource;
using Repositories.Interfaces;
using SCMSystem.Helper.Interface;
using Services.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Authorize(Roles = UserRoles.Admin)]
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
                _logger.LogInformation(MyLogEvents.InsertItem, $"Run endpoint /api/product POST Product");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.InsertItem, $"Create new Product Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
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

                    if (model == 0)
                    {
                        _logger.LogInformation(MyLogEvents.InsertItem, $"New Product entity not created");
                        return NotFound();
                    }

                    _logger.LogInformation(MyLogEvents.InsertItem, $"Created new Product entity with Id: {model}");
                    return Ok(model);
                }

                _logger.LogError(MyLogEvents.InsertItem, $"Create new Product Error, ImageFile is null, BadRequest: {BadRequest().StatusCode}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.InsertItem, $"Create Product Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        #endregion

        #region Read

        // GET: api/<ProductController>
        [HttpGet]
        [Route("GetAll")]
        public async Task<IActionResult> GetAll(int page)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/product Get Products: page = {page}");

                var model = await _productService.GetAllProducts();

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Products on page: {page}, NotFound = {NotFound().StatusCode}");
                    return NotFound();
                }

                if (model.Count == 0)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Products on page: {page}, NoContent = {NoContent().StatusCode}");
                    return NoContent();
                }

                //Pagination
                var pageResults = 3f;
                var pageCount = Math.Ceiling(model.Count / pageResults);

                var products = model
                    .Skip((page - 1) * (int)pageResults)
                    .Take((int)pageResults)
                    .ToList();

                _logger.LogInformation(MyLogEvents.ListItems, $"Get Products successfully");
                return Ok(products);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItem, $"Get Products Error: Error message = {ex.Message}");
                return BadRequest(ex?.InnerException?.Message);
            }
        }

        // GET api/<ProductController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                _logger.LogInformation(MyLogEvents.GetItem, $"Run endpoint /api/product to Get Product by id : {id}");

                var model = await _productService.GetProductById(id);

                if (model == null)
                {
                    _logger.LogWarning(MyLogEvents.GetItemNotFound, $"Get Product by id {id} NOT FOUND : {NotFound().StatusCode}");
                    return NotFound();
                }

                _logger.LogInformation(MyLogEvents.GetItem, $"Get Product by id {id}: results successful");
                return Ok(model);
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.GetItemNotFound, $"Get Product by id Error: Error message = {ex.Message}");
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
                _logger.LogInformation(MyLogEvents.UpdateItem, $"Run endpoint /api/product Update Product: id = {id}");

                if (!ModelState.IsValid)
                {
                    _logger.LogError(MyLogEvents.UpdateItem, $"Update a Product Error: ModelState: {ModelState.IsValid}, BadRequest: {BadRequest().StatusCode}");
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

                    if (model == null)
                    {
                        _logger.LogWarning(MyLogEvents.UpdateItemNotFound, $"Updated a Product by id {id} NOT FOUND : {NotFound().StatusCode}");
                        return NotFound();
                    }

                    _logger.LogInformation(MyLogEvents.UpdateItem, $"Updated a Product: id = {model.Id}");
                    return Ok(model);
                }


                _logger.LogError(MyLogEvents.UpdateItem, $"Update a Product Error, ImageFile is null, BadRequest: {BadRequest().StatusCode}");
                return BadRequest();
            }
            catch (Exception ex)
            {
                _logger.LogError(MyLogEvents.UpdateItem, $"Update a Product by id {id} Error:  Error message = {ex.Message}");
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
                _logger.LogInformation(MyLogEvents.DeleteItem, $"Run endpoint /api/product Delete Product by id: {id}");

                var model = await _productService.DeleteProduct(id);

                if (model == null || !model.IsProductDeleted) {
                    _logger.LogWarning(MyLogEvents.DeleteItem, $"Delete Product: id = {id}, NotFound : {NotFound().StatusCode}");
                    return NotFound(); 
                }


                var delete = _fileService.DeleteImage(model.ImageName);

                _logger.LogInformation(MyLogEvents.DeleteItem, $"Deleted Product: id = {id}");
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
