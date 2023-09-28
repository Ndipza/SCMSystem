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
        public Task<List<PaymentMethod>> Get()
        {
            var model = _unitOfWork.PaymentMethodRepository.GetAll();
            return model;
        }

        // GET api/<PaymentMethodController>/5
        [HttpGet("{id}")]
        public Task<PaymentMethod?> Get(int id)
        {
            var model = _unitOfWork.PaymentMethodRepository.GetById(id);
            return model;
        }

        // POST api/<PaymentMethodController>
        [HttpPost]
        public Task<long> Post([FromBody] PaymentMethodViewModel paymentMethodViewModel)
        {
            var model = _unitOfWork.PaymentMethodRepository.InsertAsync(paymentMethodViewModel);
            return model;
        }

        // PUT api/<PaymentMethodController>/5
        [HttpPut("{id}")]
        public Task<PaymentMethod> Put([FromBody] PaymentMethodViewModel paymentMethodViewModel, int id)
        {
            var model = _unitOfWork.PaymentMethodRepository.UpdateAsync(paymentMethodViewModel, id);
            return model;
        }

        // DELETE api/<PaymentMethodController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.PaymentMethodRepository.Delete(id);
        }
    }
}
