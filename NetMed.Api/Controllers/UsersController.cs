using Microsoft.AspNetCore.Mvc;
using NetMed.Persistence.Interfaces;
using NetMed.Domain.Entities;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

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
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var user = await _userRepository.GetAllAsync();
            return Ok(user);
        }

        // GET api/<UsersController>/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var users = await _userRepository.GetEntityByIdAsync(id);
            return Ok(users);
        }

        // POST api/<UsersController>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Users users)
        {
            var user = await _userRepository.SaveEntityAsync(users);

            if(user.Success)
            {
                return Ok(user);
            }
            else
            {
                return BadRequest(user);
            }
        }

        // PUT api/<UsersController>/5
        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] Users users)
        {
            var use = await _userRepository.GetAllAsync();
            return Ok(use);
        }

        // DELETE api/<UsersController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
