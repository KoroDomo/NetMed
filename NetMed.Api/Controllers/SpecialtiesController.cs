using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        // OJOCrear abstraccion para ILogger
        public SpecialtiesController(ISpecialtiesRepository specialtiesRepostiroy, ILogger<SpecialtiesController> logger) 
        {
            _specialtiesRepository = specialtiesRepostiroy;
        }
        // GET: api/<SpecialtiesController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {

            var specialties = await _specialtiesRepository.GetAllAsync(); // 22:14

            return Ok();
        }

        // GET api/<SpecialtiesController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<SpecialtiesController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<SpecialtiesController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<SpecialtiesController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
