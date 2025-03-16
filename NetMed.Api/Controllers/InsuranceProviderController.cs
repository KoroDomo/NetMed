using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Infraestructure.Logger;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InsuranceProviderController : ControllerBase
    {
        public IInsuranceProviderService _insuranceProviderService;

        public InsuranceProviderController(IInsuranceProviderService insuranceProviderService,
                                           ICustomLogger logger) 
        {
            _insuranceProviderService = insuranceProviderService;
        }
        
        [HttpGet("GetInsuranceProviders")]
        public async Task<IActionResult> Get()
        {
            var insurenceProviders = await _insuranceProviderService.GetAll();

            return Ok(insurenceProviders);
        }

        [HttpGet("GetInsuranceProviderBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var insurenceProviders = await _insuranceProviderService.GetById(id);

            return Ok(insurenceProviders);
        }


        [HttpPost("SaveInsuranceProvider")]
        public async Task<IActionResult> Post(SaveInsuranceProviderDto insuranceProvider)
        {
            var insurenceProviders = await _insuranceProviderService.Save(insuranceProvider);

            return Ok(insurenceProviders);
        }


        [HttpPut("UpdateInsuranceProvider")]
        public async Task<IActionResult> Put(UpdateInsuranceProviderDto insuranceProvider)
        {
            var insurenceProviders = await _insuranceProviderService.Update(insuranceProvider);

            return Ok(insurenceProviders);
        }

        [HttpDelete("RemoveInsuranceProvider")]
        public async Task<IActionResult> Remove(RemoveInsuranceProviderDto insuranceProvider)
        {
            var insurenceProviders = await _insuranceProviderService.Remove(insuranceProvider);

            return Ok(insurenceProviders);
        }

    }
}
