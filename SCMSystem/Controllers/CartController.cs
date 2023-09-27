using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public CartController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CartController>
        [HttpGet]
        public Task<List<Cart>> Get()
        {
            var model = _unitOfWork.CartRepository.GetAll();
            return model;
        }

        // GET api/<CartController>/5
        [HttpGet("{id}")]
        public Task<Admin?> Get(int id)
        {
            var model = _unitOfWork.AdminRepository.GetById(id);
            return model;
        }

        // POST api/<CartController>
        [HttpPost]
        public Task<long> Post([FromBody] CartViewModel cartViewModel)
        {
            var model = _unitOfWork.CartRepository.InsertAsync(cartViewModel);
            return model;
        }

        // PUT api/<CartController>/5
        [HttpPut("{id}")]
        public Task<Cart> Put([FromBody] CartViewModel cartViewModel, int id)
        {
            var model = _unitOfWork.CartRepository.UpdateAsync(cartViewModel, id);
            return model;
        }

        // DELETE api/<CartController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.CartRepository.Delete(id);
        }
    }
}
