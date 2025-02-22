using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorsRepository doctorsRepository,
            ILogger<DoctorsController> logger)
        {
            _doctorsRepository = doctorsRepository;
            _logger = logger;
        }

        // GET: api/<DoctoresControlador>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var doctors = await _doctorsRepository.GetAllAsync();
            return Ok(doctors);
        }

        // GET api/<DoctoresControlador>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _doctorsRepository.GetEntityByIdAsync(id);
            return Ok(doctor);
        }

        //api/<DoctoresControlador>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Doctors doctor)
        {
            var doc = await _doctorsRepository.SaveEntityAsync(doctor);

            if(doc.Success)
            {
                return Ok(doc);
            }
            else
            {
                return BadRequest(doc);
            } 

        }
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Doctors doctor)
        {
            var doct = await _doctorsRepository.UpdateEntityAsync(doctor);
            return Ok(doct);
        }

        // DELETE api/<DoctoresControlador>/5
        [HttpDelete("{id}")]
        public void Delete()
        {
        }
    }
}
