using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;

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
        
        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var insurenceProviders = await _InsuranceProviderRepository.GetAllAsync();

            return Ok(insurenceProviders);
        }

        [HttpGet("GetInsuranceProviderBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var insurenceProviders = await _InsuranceProviderRepository.GetEntityByIdAsync(id);

            return Ok(insurenceProviders);
        }


        [HttpPost("SaveInsuranceProvider")]
        public async Task<IActionResult> Post([FromBody] InsuranceProviders insuranceProvider)
        {
            var insurenceProviders = await _InsuranceProviderRepository.SaveEntityAsync(insuranceProvider);

            return Ok(insurenceProviders);
        }


        [HttpPost("UpdateInsuranceProvider")]
        public async Task<IActionResult> Put([FromBody] InsuranceProviders insuranceProvider)
        {
            var insurenceProviders = await _InsuranceProviderRepository.UpdateEntityAsync(insuranceProvider);

            return Ok(insurenceProviders);
        }

        
    }
}
