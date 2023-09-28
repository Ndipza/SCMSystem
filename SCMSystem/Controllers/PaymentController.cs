using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public PaymentController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<PaymentController>
        [HttpGet]
        public Task<List<Payment>> Get()
        {
            var model = _unitOfWork.PaymentRepository.GetAll();
            return model;
        }

        // GET api/<PaymentController>/5
        [HttpGet("{id}")]
        public Task<Payment?> Get(int id)
        {
            var model = _unitOfWork.PaymentRepository.GetById(id);
            return model;
        }

        // POST api/<PaymentController>
        [HttpPost]
        public Task<long> Post([FromBody] PaymentViewModel paymentViewModel)
        {
            var model = _unitOfWork.PaymentRepository.InsertAsync(paymentViewModel);
            return model;
        }

        // PUT api/<PaymentController>/5
        [HttpPut("{id}")]
        public Task<Payment> Put([FromBody] PaymentViewModel paymentViewModel, int id)
        {
            var model = _unitOfWork.PaymentRepository.UpdateAsync(paymentViewModel, id);
            return model;
        }

        // DELETE api/<PaymentController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.PaymentRepository.Delete(id);
        }
    }
}
