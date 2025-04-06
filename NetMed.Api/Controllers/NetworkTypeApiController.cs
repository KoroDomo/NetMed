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
        [HttpGet("GetNetworkTypes")]
        public async Task<IActionResult> Get()
        {
            var networkTypes = await _networkTypeService.GetAll();

            return Ok(networkTypes);
        }

        [HttpGet("GetNetworkTypeBy{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var networkType = await _networkTypeService.GetById(id);

            return Ok(networkType);
        }


        [HttpPost("SaveNetworkType")]
        public async Task<IActionResult> Post(SaveNetworkTypeDto networks)
        {
            var networkType = await _networkTypeService.Save(networks);

            return Ok(networkType);
        }


        [HttpPut("UpdateNetworkType")]
        public async Task<IActionResult> Put(UpdateNetworkTypeDto networks)
        {
            var networkType = await _networkTypeService.Update(networks);

            return Ok(networkType);
        }
        [HttpDelete("RemoveNetworkType")]
        public async Task<IActionResult> Remove(RemoveNetworkTypeDto networks)
        {
            var networkType = await _networkTypeService.Remove(networks);

            return Ok(networkType);
        }
    }
}
