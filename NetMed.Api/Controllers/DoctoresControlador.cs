using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctoresControlador : ControllerBase
    {
        private readonly IDoctorsRepository _doctorsRepository;
        private readonly ILogger<DoctoresControlador> _logger;

        public DoctoresControlador(IDoctorsRepository doctorsRepository,
            ILogger<DoctoresControlador> logger)
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
            if (doctor == null)
            {
                return NotFound();
            }
            return Ok(doctor);
        }
        
        // POST api/<DoctoresControlador>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<DoctoresControlador>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<DoctoresControlador>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
