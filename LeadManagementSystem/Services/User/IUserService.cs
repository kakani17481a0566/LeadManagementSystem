using LeadManagementSystem.ViewModel.User;
using System.Collections.Generic;
using System.Threading.Tasks;

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

    }
}
