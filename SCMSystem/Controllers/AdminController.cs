using Core.ViewModels;
using Data.Models;
using Microsoft.AspNetCore.Mvc;
using Repositories.Interfaces;

namespace SCMSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        public AdminController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        // GET: api/<AdminController>
        [HttpGet]
        public Task<List<Admin>> Get()
        {
            var model = _unitOfWork.AdminRepository.GetAll();
            return model;
        }

        // GET api/<AdminController>/5
        [HttpGet("{id}")]
        public Task<Admin?> Get(int id)
        {
            var model = _unitOfWork.AdminRepository.GetById(id);
            return model;
        }

        // POST api/<AdminController>
        [HttpPost]
        public Task<long> Post([FromBody] AdminViewModel adminViewModel)
        {
            var model = _unitOfWork.AdminRepository.InsertAsync(adminViewModel);
            return model;
        }

        // PUT api/<AdminController>/5
        [HttpPut("{id}")]
        public Task<Admin> Put([FromBody] AdminViewModel adminViewModel, int id)
        {
            var model = _unitOfWork.AdminRepository.UpdateAsync(adminViewModel, id);
            return model;
        }

        // DELETE api/<AdminController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
            _unitOfWork.AdminRepository.Delete(id);
        }
    }
}
