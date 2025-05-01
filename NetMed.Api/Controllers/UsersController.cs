using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;
using NetMed.Domain.Entities;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersService _usersServices;
        public UsersController(IUsersService usersServices,
            ILogger<UsersController> logger)
        {
            _usersServices = usersServices;
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet("GetUsers")]

        public async Task<IActionResult> Get()
        {
            var user = await _usersServices.GetAllData();
            if (user.Success == true)
            {
                return Ok(user.data);
            }
            return Ok();
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var  result = await _usersServices.GetById(id);

            return Ok(result);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> AddUser([FromBody] AddUserDto usersDto)
        {
            if (usersDto == null)
            {
                return BadRequest("User data is required.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usersServices.Add(usersDto);
            if (!result.Success)
            {
                return BadRequest(result.Message);
            }

            return Ok(result.data);
        }
        // PUT api/<UsersController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto usersDto) 
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usersServices.Update(usersDto);
            if (result.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.Message);
        }
        // DELETE api/<UsersController>/5
        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAsync([FromBody] DeleteUserDto usersDto) // Updated parameter type
        {
            if (usersDto == null)
            {
                return BadRequest("User data is required.");
            }

            var usuario = await _usersServices.Delete(usersDto);
            return Ok(usuario);
        }
    }
}
