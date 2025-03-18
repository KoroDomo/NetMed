using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.Doctors;


// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorsController : ControllerBase
    {
        private readonly IDoctorsServices _doctorsServices;
        private readonly ILogger<DoctorsController> _logger;

        public DoctorsController(IDoctorsServices doctorsServices, ILogger<DoctorsController> logger)
      
        {
            _doctorsServices = doctorsServices;
            _logger = logger;
        }

        // GET: api/<DoctoresControlador>
        [HttpGet("GetDoctors")]
        public async Task<IActionResult> Get()
        {
            var res = await _doctorsServices.GetAllData();
            if (res.Success ==true)
            {
                return Ok(res.data);
            }
            return Ok();

        }

        // GET api/<DoctoresControlador>/5
        [HttpGet("GetDoctorsById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var doctor = await _doctorsServices.GetById(id);
            return Ok(doctor);
        }

        //api/<DoctoresControlador>
        [HttpPost("SaveDoctor")]
        public async Task<IActionResult> Post([FromBody] AddDoctorsDto doctorsDto)
        {

            var result = await _doctorsServices.Add(doctorsDto);
            if (result.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.Message);
        }

        [HttpPut("UpdateDoctor/{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UpdateDoctorsDto doctorsDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            doctorsDto.UserId = id; // Ensure the ID is set correctly
            var result = await _doctorsServices.Update(doctorsDto);
            if (result.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.Message);
        }

        // DELETE api/<DoctoresControlador>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult>  DeleteAsync(DeleteDoctorDto doctorDto)
        {
            var doc = await _doctorsServices.Delete(doctorDto);
            return Ok(doc);
        }

      
    }
}
