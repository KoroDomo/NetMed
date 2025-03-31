using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.NetworkType;
using NetMed.Infraestructure.Logger;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeApiController : ControllerBase
    {
        public INetworkTypeService _networkTypeService;

        public NetworkTypeApiController(INetworkTypeService networkTypeRepository,
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
        public async Task<IActionResult> Post(SaveNetworkTypeDto networks)
        {
            var networkTypeRepositorys = await _networkTypeService.Save(networks);

            return Ok(networkTypeRepositorys);
        }


        [HttpPut("UpdateNetworkTypeRepository")]
        public async Task<IActionResult> Put(UpdateNetworkTypeDto networks)
        {
            var networkTypeRepositorys = await _networkTypeService.Update(networks);

            return Ok(networkTypeRepositorys);
        }
        [HttpDelete("RemoveInsuranceProvider")]
        public async Task<IActionResult> Remove(RemoveNetworkTypeDto networks)
        {
            var networkType = await _networkTypeService.Remove(networks);

            return Ok(networkType);
        }
    }
}
