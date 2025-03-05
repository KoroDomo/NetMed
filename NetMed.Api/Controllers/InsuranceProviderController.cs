using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Validators;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProviderController : ControllerBase
    {
        public IInsuranceProviderRepository _insuranceProviderService;

        public InsuranceProviderController(IInsuranceProviderRepository insuranceProviderRepository,
                                           ICustomLogger logger) 
        {
            _insuranceProviderService = insuranceProviderRepository;
        }
        
        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var insurenceProviders = await _insuranceProviderService.GetAllAsync();

            return Ok(insurenceProviders);
        }

        [HttpGet("GetInsuranceProviderBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var insurenceProviders = await _insuranceProviderService.GetEntityByIdAsync(id);

            return Ok(insurenceProviders);
        }


        [HttpPost("SaveInsuranceProvider")]
        public async Task<IActionResult> Post([FromBody] InsuranceProviders insuranceProvider)
        {
            var insurenceProviders = await _insuranceProviderService.SaveEntityAsync(insuranceProvider);

            return Ok(insurenceProviders);
        }


        [HttpPost("UpdateInsuranceProvider")]
        public async Task<IActionResult> Put([FromBody] InsuranceProviders insuranceProvider)
        {
            var insurenceProviders = await _insuranceProviderService.UpdateEntityAsync(insuranceProvider);

            return Ok(insurenceProviders);
        }

        
    }
}
