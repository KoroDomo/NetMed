using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProviderController : ControllerBase
    {
        public IInsuranceProviderRepository _InsuranceProviderRepository;

        public InsuranceProviderController(IInsuranceProviderRepository insuranceProviderRepository,
                                           ILogger<InsuranceProviderController> logger) 
        {
            _InsuranceProviderRepository = insuranceProviderRepository;
        }
        // GET: api/<InsuranceProviderController>
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var insurenceProviders = await _InsuranceProviderRepository.GetAllAsync();

            return Ok(insurenceProviders);
        }

        // GET api/<InsuranceProviderController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<InsuranceProviderController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<InsuranceProviderController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<InsuranceProviderController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
