using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;
using NetMed.Domain.Entities;
using NetMed.Persistence.Repositories;

namespace NetMed.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly ILogger<UsersController> _logger;
        private readonly IUsersRepository _userRepository;
            
        public UsersController(IUsersRepository usersRepository,
            ILogger<UsersController> logger)
        {
            _userRepository = usersRepository;
            _logger = logger;
        }

        // GET: api/<UsersController>
        [HttpGet("GetUsers")]

        public async Task<IActionResult> Get()
        {
            var user = await _userRepository.GetAllAsync();
            return Ok(user);
        }

        // GET api/<UsersController>/5
        [HttpGet("GetUserById")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _userRepository.GetEntityByIdAsync(id);
            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UsersModel users)
        {

            var result = await _userRepository.SaveEntityAsync(users);
            return Ok(users);

        }
            // PUT api/<UsersController>/5
            [HttpPut("Update")]
        public async Task<IActionResult> Put([FromBody] UsersModel users)
        {
            var use = await _userRepository.GetAllAsync();
            return Ok(use);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("Delete")]
        public async Task<IActionResult> DeleteAsync(UsersModel users)
        {
            var usuario = await _userRepository.DeleteEntityAsync(users);
            return Ok(usuario);
        }
    }
}
