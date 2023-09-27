using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StatusController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public StatusController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<StatusController>
        [HttpGet]
        public Task<List<Status>> Get()
        {
            var model = _unitOfWork.StatusRepository.GetAll();
            return model;
        }

        // GET api/<StatusController>/5
        [HttpGet("{id}")]
        public Task<Status?> Get(int id)
        {
            var model = _unitOfWork.StatusRepository.GetById(id);
            return model;
        }

        // POST api/<StatusController>
        [HttpPost]
        public Task<long> Post([FromBody] StatusViewModel statusViewModel)
        {
            var model = _unitOfWork.StatusRepository.InsertAsync(statusViewModel);
            return model;
        }

        // PUT api/<StatusController>/5
        [HttpPut("{id}")]
        public Task<Status> Put([FromBody] StatusViewModel statusViewModel, int id)
        {
            var model = _unitOfWork.StatusRepository.UpdateAsync(statusViewModel, id);
            return model;
        }

        // DELETE api/<StatusController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.StatusRepository.Delete(id);
        }
    }
}
