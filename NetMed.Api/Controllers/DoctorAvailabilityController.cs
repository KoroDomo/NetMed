using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;

        public DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository, ILogger<DoctorAvailability> logger ) 
        {
           _doctorAvailabilityRepository = doctorAvailabilityRepository;
        }  
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var doctorAvailability = await _doctorAvailabilityRepository.GetAllAsync();
            return Ok(doctorAvailability);
        }
        [HttpGet("GetAvailabilityById")]
        public async Task<IActionResult> Get(int id)
        {
            var doctorAvailability = await _doctorAvailabilityRepository.GetEntityByIdAsync(id);
            return Ok(doctorAvailability);
        }
        [HttpPost("SaveDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailability doctorAvaility)
        {
            var doctorAvailability = await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvaility);
            return Ok(doctorAvailability);
        }
        [HttpPost("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put([FromBody] DoctorAvailability doctorAvaility)
        {
            var doctorAvailability = await _doctorAvailabilityRepository.SaveEntityAsync(doctorAvaility);
            return Ok(doctorAvailability);
        }
    }
}
