using Microsoft.AspNetCore.Mvc;
using NetMed.Domain.Entities;
using NetMed.Persistence.Interfaces;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialtiesController : ControllerBase
    {
        private readonly ISpecialtiesRepository _specialtiesRepository;
        public SpecialtiesController(ISpecialtiesRepository specialtiesRepostiroy, ILogger<SpecialtiesController> logger) 
        {
            _specialtiesRepository = specialtiesRepostiroy;
        }

        [HttpGet("GetSpecialties")]
        public async Task<IActionResult> Get()
        {

            var specialties = await _specialtiesRepository.GetAllAsync();

            return Ok(specialties);
        }

        [HttpGet("GetSpecialtyById")]
        public async Task<IActionResult> Get(short id)
        {
            var specialties = await _specialtiesRepository.GetEntityByIdAsync(id);
            return Ok(specialties);
        }

        [HttpPost("SaveSpecialty")]
        public async Task<IActionResult> Post([FromBody] Specialties specialty)
        {
            var specialties = await _specialtiesRepository.SaveEntityAsync(specialty);
            return Ok(specialties);
        }

        [HttpPost("UpdateSpecialty")]
        public async Task<IActionResult> Put([FromBody] Specialties specialty)
        {
            var specialties = await _specialtiesRepository.UpdateEntityAsync(specialty);
            return Ok(specialties);
        }

    }
}
