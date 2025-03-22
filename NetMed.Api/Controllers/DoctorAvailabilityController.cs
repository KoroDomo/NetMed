using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.DoctorAvailability;
using NetMed.Application.Interfaces;
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
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;
        private readonly IMessageService _messageService;
        private readonly ILoggerSystem _logger;

        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService, ILoggerSystem logger, IMessageService messageService) 
        {
            _doctorAvailabilityService = doctorAvailabilityService;
            _messageService = messageService;
            _logger = logger;
        }  
        [HttpGet("GetDoctorAvailability")]
        public async Task<IActionResult> Get()
        {
            var doctorAvailability = await _doctorAvailabilityService.GetAll();
            return Ok(doctorAvailability);
        }
        [HttpGet("GetAvailabilityById")]
        public async Task<IActionResult> Get(int id)
        {
            var doctorAvailability = await _doctorAvailabilityService.GetById(id);
            return Ok(doctorAvailability);
        }
        [HttpPost("SaveDoctorAvailability")]
        public async Task<IActionResult> Post([FromBody] DoctorAvailabilityDto doctorAvaility)
        {
            var saveDoctorAvailability = new SaveDoctorAvailabilityDto();

            var doctorAvailability = await _doctorAvailabilityService.Save(saveDoctorAvailability);
            return Ok(doctorAvailability);
        }
        [HttpPost("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put([FromBody] DoctorAvailabilityDto doctorAvaility)
        {
            var updateDoctorAvailability = new UpdateDoctorAvailabilityDto();

            var doctorAvailability = await _doctorAvailabilityService.Update(updateDoctorAvailability);
            return Ok(doctorAvailability);
        }
    }
}
