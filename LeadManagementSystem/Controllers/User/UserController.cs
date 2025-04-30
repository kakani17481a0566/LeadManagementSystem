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

        // Constructor to inject dependencies
        public UserController(IUserService userService, ILogger<UserController> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        // GET: api/User
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

        // GET: api/User/{id}
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

        // POST: api/User
        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] UserCreateViewModel user)
        {
            if (user == null)
            {
                return BadRequest("User data is required.");
            }

            if (string.IsNullOrEmpty(user.LoginId) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("LoginId and Password are required.");
            }

            var roleExists = await _userService.RoleExistsAsync(user.RoleId);
            if (!roleExists)
            {
                return BadRequest("RoleId is invalid.");
            }

            _logger.LogInformation("Creating user: {LoginId}", user.LoginId);
            var createdUser = await _userService.CreateUserAsync(user);

            if (createdUser == null)
            {
                return BadRequest("Failed to create user.");
            }

            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }

        // POST: api/User/login
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromForm] string identifier, [FromForm] string password)
        {
            _logger.LogInformation("Attempting login for {Identifier}", identifier);
            var (isSuccessful, message) = await _userService.LoginAsync(identifier, password);

            if (isSuccessful)
            {
                return Ok(new { success = true, message = message ?? "Login successful." });
            }

            _logger.LogWarning("Failed login attempt for {Identifier}: {Message}", identifier, message);
            return Unauthorized(new { success = false, message = message ?? "Invalid credentials." });
        }

        // PUT: api/User/{id}
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] UserViewModel user)
        {
            if (user == null || id != user.Id)
            {
                return BadRequest("Invalid user data or ID mismatch.");
            }

            if (string.IsNullOrEmpty(user.LoginId) || string.IsNullOrEmpty(user.Password))
            {
                return BadRequest("LoginId and Password are required.");
            }

            _logger.LogInformation("Updating user with ID {UserId}", id);
            var success = await _userService.UpdateUserAsync(id, user);

            if (!success)
            {
                return NotFound($"User with ID {id} not found.");
            }

            return NoContent();
        }

        // DELETE: api/User/{id}
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
    }
}
