using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.InsuranceProvider;
using NetMed.Infraestructure.Logger;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
        public INetworkTypeService _networkTypeService;

        public NetworkTypeController(INetworkTypeService networkTypeRepository,
                                           ICustomLogger logger)
        {
            _networkTypeService = networkTypeRepository;
        }
        [HttpGet("GetNetworkTypeRepositorys")]
        public async Task<IActionResult> Get()
        {
            var networkTypeRepositorys = await _networkTypeService.GetAll();

            return Ok(networkTypeRepositorys);
        }

        [HttpGet("GetNetworkTypeRepositoryBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var networkTypeRepositorys = await _networkTypeService.GetById(id);

            return Ok(networkTypeRepositorys);
        }


        [HttpPost("SaveNetworkTypeRepository")]
        public async Task<IActionResult> Post([FromBody] SaveNetworkTypeDto networks)
        {
            var networkTypeRepositorys = await _networkTypeService.Save(networks);

            return Ok(networkTypeRepositorys);
        }


        [HttpPut("UpdateNetworkTypeRepository")]
        public async Task<IActionResult> Put([FromBody] UpdateNetworkTypeDto networks)
        {
            var networkTypeRepositorys = await _networkTypeService.Update(networks);

            return Ok(networkTypeRepositorys);
        }
        [HttpDelete("RemoveInsuranceProvider")]
        public async Task<IActionResult> Remove([FromBody] RemoveNetworkTypeDto insuranceProvider)
        {
            var insurenceProviders = await _networkTypeService.Remove(insuranceProvider);

            return Ok(insurenceProviders);
        }
    }
}
