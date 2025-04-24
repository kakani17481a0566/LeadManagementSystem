using LeadManagementSystem.Models; // Make sure this is included, as RolesModel is part of this namespace
using Microsoft.EntityFrameworkCore; // For ToListAsync and FirstOrDefaultAsync
using LeadManagementSystem.ViewModel.Role;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LeadManagementSystem.Data;

namespace LeadManagementSystem.Services.Role
{
    public class RoleService : IRoleService
    {
        private readonly ApplicationDbContext _context;

        public RoleService(ApplicationDbContext context)
        {
            _context = context;
        }

        // Fetch all roles and return as RoleViewModel
        public async Task<List<RoleViewModel>> GetAllRolesAsync()
        {
            var roles = await _context.Roles
                                      .Select(role => new RoleViewModel
                                      {
                                          Id = role.Id,
                                          Name = role.Name
                                      })
                                      .ToListAsync();  // Asynchronous operation to fetch the data

            return roles;
        }

        // Create a new role
        public async Task CreateRoleAsync(RoleViewModel model)
        {
            var role = new RolesModel  // Use RolesModel here
            {
                Name = model.Name
            };

            _context.Roles.Add(role);  // Adding the new role to the database
            await _context.SaveChangesAsync();  // Save the changes to the database
        }

        // Update an existing role
        public async Task UpdateRoleAsync(RoleViewModel model)
        {
            var role = await _context.Roles.FindAsync(model.Id);

            if (role != null)
            {
                role.Name = model.Name;
                _context.Roles.Update(role);  // Update the role in the database
                await _context.SaveChangesAsync();  // Save the changes to the database
            }
        }

        // Delete a role by ID
        public async Task<bool> DeleteRoleAsync(int id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role != null)
            {
                _context.Roles.Remove(role);  // Remove the role from the database
                await _context.SaveChangesAsync();  // Save changes
                return true;
            }

            return false;
        }

        // Fetch a specific role by ID
        public async Task<RoleViewModel> GetRoleByIdAsync(int id)
        {
            var role = await _context.Roles
                                      .Where(r => r.Id == id)
                                      .Select(r => new RoleViewModel
                                      {
                                          Id = r.Id,
                                          Name = r.Name
                                      })
                                      .FirstOrDefaultAsync();  // Fetch the first matching role asynchronously

            return role;
        }
    }
}
