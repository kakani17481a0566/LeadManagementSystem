using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.User;
using Microsoft.EntityFrameworkCore;

namespace LeadManagementSystem.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        // Constructor to inject ApplicationDbContext
        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Check if a Role exists by ID
        public async Task<bool> RoleExistsAsync(int roleId)
        {
            return await _context.Roles.AnyAsync(r => r.Id == roleId);
        }

        // Get a list of all users
        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)  // Including related Role entity
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name  // Mapping Role Name
                })
                .ToListAsync();  // Asynchronously fetching the list of users
        }

        // Get a specific user by ID
        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)  // Including related Role entity
                .Where(u => u.Id == id)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name  // Mapping Role Name
                })
                .FirstOrDefaultAsync();  // Fetching the user by ID
        }

        // Implement the method to create a user
        public async Task<UserModel> CreateUserAsync(UserCreateViewModel userCreateViewModel)
        {
            var user = new UserModel
            {
                FirstName = userCreateViewModel.FirstName,
                LastName = userCreateViewModel.LastName,
                Email = userCreateViewModel.Email,
                Phone = userCreateViewModel.Phone,
                LoginId = userCreateViewModel.LoginId,
                Password = userCreateViewModel.Password, // Plain-text password
                RoleId = userCreateViewModel.RoleId
            };

            _context.Users.Add(user);  // Add user to context
            await _context.SaveChangesAsync();  // Save changes to database

            // Return the created user with the auto-generated ID
            return user;
        }



        // Update an existing user
        public async Task<bool> UpdateUserAsync(int id, UserViewModel userViewModel)
        {
            var user = await _context.Users.FindAsync(id);  // Find user by ID
            if (user == null) return false;  // If not found, return false

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Phone = userViewModel.Phone;
            user.LoginId = userViewModel.LoginId;

            if (!string.IsNullOrEmpty(userViewModel.Password))
            {
                user.Password = userViewModel.Password;  // Update password if provided
            }

            user.RoleId = userViewModel.RoleId;

            _context.Users.Update(user);  // Update the user
            await _context.SaveChangesAsync();  // Save changes
            return true;
        }

        // Delete a user by ID
        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);  // Find user by ID
            if (user == null) return false;  // If not found, return false

            _context.Users.Remove(user);  // Remove user from context
            await _context.SaveChangesAsync();  // Save changes to the database
            return true;
        }

        // Handle user login with plain-text password
        public async Task<(bool isSuccessful, string message)> LoginAsync(string identifier, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)  // Include Role entity to get Role Name
                .FirstOrDefaultAsync(u =>
                    u.Email == identifier ||  // Check if identifier matches email
                    u.Phone == identifier ||  // Check if identifier matches phone number
                    u.LoginId == identifier);  // Check if identifier matches loginId

            if (user == null)
                return (false, "User not found");

            if (user.Password != password)  // Check if the provided password matches
                return (false, "Invalid password");

            return (true, "Login successful");  // Return success if credentials match
        }
    }
}
