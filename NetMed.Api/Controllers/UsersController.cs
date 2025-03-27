using Microsoft.AspNetCore.Mvc;
using NetMed.Application.Contracts;
using NetMed.Application.Dtos.UsersDto;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersServices _usersServices;
        public UsersController(IUsersServices usersServices,
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
            return Ok(user);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById/{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _usersServices.GetById(id);
            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AddUserDto usersDto) // Updated parameter type
        {

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var result = await _usersServices.Add(usersDto);
            if (result.Success)
            {
                return Ok(result.data);
            }
            return BadRequest(result.Message);
        }
        // PUT api/<UsersController>/5
        [HttpPut("Update/{id}")]
        public async Task<IActionResult> Put([FromBody] UpdateUserDto usersDto) // Updated parameter type
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
        public async Task<IActionResult> DeleteAsync(DeleteUserDto usersDto) // Updated parameter type
        {
            var usuario = await _usersServices.Delete(usersDto);
            return Ok(usuario);
        }
    }
}
