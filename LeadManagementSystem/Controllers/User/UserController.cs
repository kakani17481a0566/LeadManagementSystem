using LeadManagementSystem.Services.User;
using LeadManagementSystem.ViewModel.User;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Linq;
using System.Threading.Tasks;

namespace LeadManagementSystem.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly ILogger<UserController> _logger;

        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            _logger.LogInformation("Fetching all users.");
            var users = await _userService.GetUsersAsync();

            if (users == null || !users.Any())
            {
                return NotFound("No users found.");
            }

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            _logger.LogInformation("Fetching user with ID {UserId}", id);
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            _logger.LogInformation("Creating user: {LoginId}", user.LoginId);
            var success = await _userService.CreateUserAsync(user);

            if (!success)
            {
                return BadRequest("Failed to create user.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserViewModel user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("Invalid user data or ID mismatch.");
            }

            _logger.LogInformation("Updating user with ID {UserId}", id);
            var success = await _userService.UpdateUserAsync(id, user);

            if (!success)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            _logger.LogInformation("Deleting user with ID {UserId}", id);
            var success = await _userService.DeleteUserAsync(id);

            if (!success)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string identifier, [FromForm] string password)
        {
            var (isSuccessful, message) = await _userService.LoginAsync(identifier, password);

            if (isSuccessful)
            {
                return Ok(new { success = true, message = message ?? "Login successful." });
            }

            return BadRequest(new { success = false, message = message ?? "Invalid credentials." });
        }
    }
}
    