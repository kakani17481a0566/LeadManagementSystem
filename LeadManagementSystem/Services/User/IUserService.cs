using System.Threading.Tasks;
using LeadManagementSystem.ViewModel.User;

namespace LeadManagementSystem.Services.User
{
    public interface IUserService
    {
        Task<List<UserViewModel>> GetUsersAsync();
        Task<UserViewModel> GetUserByIdAsync(int id);
        Task<bool> CreateUserAsync(UserViewModel userViewModel);
        Task<bool> UpdateUserAsync(int id, UserViewModel userViewModel);
        Task<bool> DeleteUserAsync(int id);
        Task<(bool isSuccessful, string message)> LoginAsync(string identifier, string password);

        Task<bool> RoleExistsAsync(int roleId);
    }
}
