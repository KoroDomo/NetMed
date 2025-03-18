using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Infraestructure.Logger;
using NetMed.Infraestructure.Messages;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityRepository _doctorAvailabilityRepository;
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public DoctorAvailabilityController(IDoctorAvailabilityRepository doctorAvailabilityRepository, ILoggerSystem logger, IMessageService messageService) 
        {
           _doctorAvailabilityRepository = doctorAvailabilityRepository;
            _messageService = messageService;
            _logger = logger;
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
            var doctorAvailability = await _doctorAvailabilityRepository.UpdateEntityAsync(doctorAvaility);
            return Ok(doctorAvailability);
        }
    }
}
