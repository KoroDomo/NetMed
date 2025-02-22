using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PatientsController : ControllerBase
    {
        private readonly IPatientsRepository _patientsRepository;
        private readonly ILogger<PatientsController> _logger;
        public PatientsController(IPatientsRepository patientsRepository,
            ILogger<PatientsController> logger)
        {
            _patientsRepository = patientsRepository;
            _logger = logger;
        }
        // GET: api/<PatientsController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
           var patients = await _patientsRepository.GetAllAsync();
            return Ok(patients);
        }

        // GET api/<PatientsController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var patient = await _patientsRepository.GetEntityByIdAsync(id);
            return Ok(patient);
        }


        // POST api/<PatientsController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Patients patients)
        {
            var pacient = await _patientsRepository.SaveEntityAsync(patients);
            return Ok(pacient);
        }

        // PUT api/<PatientsController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Patients patients)

        {
            var pacients = await _patientsRepository.UpdateEntityAsync(patients);
            return Ok(pacients);
        }

        // DELETE api/<PatientsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
