using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Dtos.DoctorAvailability;
using NetMed.Application.Interfaces;


namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorAvailabilityController : ControllerBase
    {
        private readonly IDoctorAvailabilityService _doctorAvailabilityService;
        public DoctorAvailabilityController(IDoctorAvailabilityService doctorAvailabilityService) 
        {
            _doctorAvailabilityService = doctorAvailabilityService;
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
        public async Task<IActionResult> Post(SaveDoctorAvailabilityDto saveDoctorAvailability)
        {
            var doctorAvailability = await _doctorAvailabilityService.Save(saveDoctorAvailability);
            return Ok(doctorAvailability);
        }
        [HttpPut("UpdateDoctorAvailability")]
        public async Task<IActionResult> Put(UpdateDoctorAvailabilityDto updateDoctorAvailability)
        {
            var doctorAvailability = await _doctorAvailabilityService.Update(updateDoctorAvailability);
            return Ok(doctorAvailability);
        }
        [HttpDelete("RemoveDoctorAvailability")]
        public async Task<IActionResult> Remove(RemoveDoctorAvailabilityDto removeDoctorAvailabilityDto)
        {
            var doctorAvailability = await _doctorAvailabilityService.Remove(removeDoctorAvailabilityDto);
            return Ok(doctorAvailability);
        }
    }
}
