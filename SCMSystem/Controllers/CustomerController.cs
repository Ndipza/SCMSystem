using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;
using System.Runtime.InteropServices;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly IUnitOfWorkRepository _unitOfWork;
        public CustomerController(IUnitOfWorkRepository unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<CustomerController>
        [HttpGet]
        public Task<List<Customer>> Get()
        {
            var model = _unitOfWork.CustomerRepository.GetAll();
            return model;
        }

        // GET api/<CustomerController>/5
        [HttpGet("{id}")]
        public Task<Customer?> Get(Guid id)
        {
            var model = _unitOfWork.CustomerRepository.GetById(id);
            return model;
        }

        // POST api/<CustomerController>
        [HttpPost]
        public Task<Guid> Post([FromBody] CustomerViewModel customerViewModel)
        {
            var model = _unitOfWork.CustomerRepository.InsertAsync(customerViewModel);
            return model;
        }

        // PUT api/<CustomerController>/5
        [HttpPut("{id}")]
        public Task<Customer> Put([FromBody] CustomerViewModel customerViewModel, Guid id)
        {
            var model = _unitOfWork.CustomerRepository.UpdateAsync(customerViewModel, id);
            return model;
        }

        // DELETE api/<CustomerController>/5
        [HttpDelete("{id}")]
        public void Delete(Guid id)
        {
            _unitOfWork.CustomerRepository.Delete(id);
        }
    }
}
