using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NetworkTypeController : ControllerBase
    {
        public INetworkTypeRepository _NetworkTypeRepository;

        public NetworkTypeController(INetworkTypeRepository networkTypeRepository,
                                           ILogger<NetworkTypeController> logger)
        {
            _NetworkTypeRepository = networkTypeRepository;
        }
        [HttpGet("GetNetworkTypeRepositorys")]
        public async Task<IActionResult> Get()
        {
            var networkTypeRepositorys = await _NetworkTypeRepository.GetAllAsync();

            return Ok(networkTypeRepositorys);
        }

        [HttpGet("GetNetworkTypeRepositoryBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var networkTypeRepositorys = await _NetworkTypeRepository.GetEntityByIdAsync(id);

            return Ok(networkTypeRepositorys);
        }


        [HttpPost("SaveNetworkTypeRepository")]
        public async Task<IActionResult> Post([FromBody] NetworkTypes networks)
        {
            var networkTypeRepositorys = await _NetworkTypeRepository.SaveEntityAsync(networks);

            return Ok(networkTypeRepositorys);
        }


        [HttpPost("UpdateNetworkTypeRepository")]
        public async Task<IActionResult> Put([FromBody] NetworkTypes networks)
        {
            var networkTypeRepositorys = await _NetworkTypeRepository.UpdateEntityAsync(networks);

            return Ok(networkTypeRepositorys);
        }
    }
}
