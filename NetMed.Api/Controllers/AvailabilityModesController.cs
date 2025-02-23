using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;
using NetMed.Persistence.Repositories;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AvailabilityModesController : ControllerBase
    {
        private readonly IAvailabilityModesRepository _availabilityModesRepository;

        public AvailabilityModesController(IAvailabilityModesRepository availabilityModesRepository, ILogger<AvailabilityModesRepository> logger)
        {
            _availabilityModesRepository = availabilityModesRepository;
        }

        [HttpGet("GetAvailabilityModes")]
        public async Task<IActionResult> Get()
        {
            var availability = await _availabilityModesRepository.GetAllAsync();
            return Ok(availability);
        }

        [HttpGet("GetAvailabilityModeById")]
        public async Task<IActionResult> Get(short id)
        {
            var availability = await _availabilityModesRepository.GetEntityByIdAsync(id);
            return Ok(availability);
        }


        [HttpPost("SaveAvailabilityModes")]
        public async Task<IActionResult> Post([FromBody] AvailabilityModes availability )
        {
            var availabilities = await _availabilityModesRepository.SaveEntityAsync(availability);
            return Ok(availabilities);
        }

     
        [HttpPost("UpdateAvailabilityModes")]
        public async Task<IActionResult> Put([FromBody] AvailabilityModes availability)
        {
            var availabilities = await _availabilityModesRepository.UpdateEntityAsync(availability);
            return Ok(availabilities);
            
        }

    }
}
