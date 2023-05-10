using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using web_api_task.Database;
using web_api_task.Models;
using web_api_task.Repositories;

namespace web_api_task.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;
        private readonly IUserGroupRepository _userGroupRepository;
        private readonly IUserStateRepository _userStateRepository;

        public UserController(IUserRepository userRepository, IUserGroupRepository userGroupRepository, IUserStateRepository userStateRepository)
        {
            _userRepository = userRepository;
            _userGroupRepository = userGroupRepository;
            _userStateRepository = userStateRepository;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<User>>> GetAll()
        {
            return Ok(await _userRepository.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<User>> Get(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            return Ok(user);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult<User>> Create([FromBody] User request)
        {
            var existingUser = await _userRepository.GetByLoginAsync(request.Login);
            if (existingUser != null)
            {
                return BadRequest($"User with login {request.Login} already exists.");
            }

            var adminUserGroup = await _userGroupRepository.GetByCodeAsync("Admin");
            if (adminUserGroup == null)
            {
                return BadRequest("Admin user group not found.");
            }

            var activeUserState = await _userStateRepository.GetByCodeAsync("Active");
            if (activeUserState == null)
            {
                return BadRequest("Active user state not found.");
            }

            var user = new User
            {
                Login = request.Login,
                Password = request.Password,
                CreatedDate = DateTime.UtcNow,
                UserGroupId = adminUserGroup.Id,
                UserStateId = activeUserState.Id
            };

            await _userRepository.CreateUserAsync(user);

            return CreatedAtAction(nameof(Get), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult> Update(int id, [FromBody] User request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.Password = request.Password;

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var blockedUserState = await _userStateRepository.GetByCodeAsync("Blocked");
            if (blockedUserState == null)
            {
                return BadRequest("Blocked user state not found.");
            }

            user.UserStateId = blockedUserState.Id;

            await _userRepository.UpdateUserAsync(user);

            return NoContent();
        }
    }
}
