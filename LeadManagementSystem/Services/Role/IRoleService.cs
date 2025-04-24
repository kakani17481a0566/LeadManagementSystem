using LeadManagementSystem.ViewModel.Role;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services.Role
{
    public interface IRoleService
    {
        Task<List<RoleViewModel>> GetAllRolesAsync();
        Task CreateRoleAsync(RoleViewModel model);
        Task UpdateRoleAsync(RoleViewModel model);
        Task<bool> DeleteRoleAsync(int id);
        Task<RoleViewModel> GetRoleByIdAsync(int id);
    }
}
