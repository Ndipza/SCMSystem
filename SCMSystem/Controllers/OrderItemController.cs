using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderItemController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrderItemController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<OrderItemController>
        [HttpGet]
        public Task<List<OrderItem>> Get()
        {
            var model = _unitOfWork.OrderItemRepository.GetAll();
            return model;
        }

        // GET api/<OrderItemController>/5
        [HttpGet("{id}")]
        public Task<OrderItem?> Get(int id)
        {
            var model = _unitOfWork.OrderItemRepository.GetById(id);
            return model;
        }

        // POST api/<OrderItemController>
        [HttpPost]
        public Task<long> Post([FromBody] OrderItemViewModel orderItemViewModel)
        {
            var model = _unitOfWork.OrderItemRepository.InsertAsync(orderItemViewModel);
            return model;
        }

        // PUT api/<OrderItemController>/5
        [HttpPut("{id}")]
        public Task<OrderItem> Put([FromBody] OrderItemViewModel orderItemViewModel, int id)
        {
            var model = _unitOfWork.OrderItemRepository.UpdateAsync(orderItemViewModel, id);
            return model;
        }

        // DELETE api/<OrderItemController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.OrderItemRepository.Delete(id);
        }
    }
}
