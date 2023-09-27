using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<OrderController>
        [HttpGet]
        public Task<List<Order>> Get()
        {
            var model = _unitOfWork.OrderRepository.GetAll();
            return model;
        }

        // GET api/<OrderController>/5
        [HttpGet("{id}")]
        public Task<Order?> Get(int id)
        {
            var model = _unitOfWork.OrderRepository.GetById(id);
            return model;
        }

        // POST api/<OrderController>
        [HttpPost]
        public Task<long> Post([FromBody] OrderViewModel orderViewModel)
        {
            var model = _unitOfWork.OrderRepository.InsertAsync(orderViewModel);
            return model;
        }

        // PUT api/<OrderController>/5
        [HttpPut("{id}")]
        public Task<Order> Put([FromBody] OrderViewModel orderViewModel, int id)
        {
            var model = _unitOfWork.OrderRepository.UpdateAsync(orderViewModel, id);
            return model;
        }

        // DELETE api/<OrderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.OrderRepository.Delete(id);
        }
    }
}
