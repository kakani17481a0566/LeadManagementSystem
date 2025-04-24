using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.User;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services.User
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<UserViewModel>> GetUsersAsync()
        {
            return await _context.Users
                .Include(u => u.Role)
                .Select(user => new UserViewModel
                {
                    Id = user.Id,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Email = user.Email,
                    Phone = user.Phone,
                    LoginId = user.LoginId,
                    RoleId = user.RoleId,
                    RoleName = user.Role.Name
                })
                .ToListAsync();
        }

        public async Task<UserViewModel> GetUserByIdAsync(int id)
        {
            return await _context.Users
                .Include(u => u.Role)
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
                    RoleName = user.Role.Name
                })
                .FirstOrDefaultAsync();
        }

        public async Task<bool> CreateUserAsync(UserViewModel userViewModel)
        {
            var user = new UserModel
            {
                FirstName = userViewModel.FirstName,
                LastName = userViewModel.LastName,
                Email = userViewModel.Email,
                Phone = userViewModel.Phone,
                LoginId = userViewModel.LoginId,
                Password = userViewModel.Password, // Plain text password
                RoleId = userViewModel.RoleId
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateUserAsync(int id, UserViewModel userViewModel)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            user.FirstName = userViewModel.FirstName;
            user.LastName = userViewModel.LastName;
            user.Email = userViewModel.Email;
            user.Phone = userViewModel.Phone;
            user.LoginId = userViewModel.LoginId;

            if (!string.IsNullOrEmpty(userViewModel.Password))
            {
                user.Password = userViewModel.Password; // Plain text password
            }

            user.RoleId = userViewModel.RoleId;

            _context.Users.Update(user);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null) return false;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return true;
        }

        // Plain-text login logic
        public async Task<(bool isSuccessful, string message)> LoginAsync(string identifier, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Email == identifier ||
                    u.Phone == identifier ||
                    u.LoginId == identifier);

            if (user == null)
                return (false, "User not found");

            if (user.Password != password)
                return (false, "Invalid password");

            return (true, "Login successful");
        }
    }
}
