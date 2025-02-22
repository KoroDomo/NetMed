using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MedicalRecordsController : ControllerBase
    {
        private readonly IMedicalRecordsRepository _medicalRecordsRepository;

        public MedicalRecordsController(IMedicalRecordsRepository medicalRecordsRepository, ILogger<MedicalRecordsRepository> logger) 
        {
            _medicalRecordsRepository = medicalRecordsRepository;
        }
        // GET: api/<MedicalRecordsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var medical = await _medicalRecordsRepository.GetAllAsync();
            return Ok();
        }

        // GET api/<MedicalRecordsController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<MedicalRecordsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<MedicalRecordsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<MedicalRecordsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
