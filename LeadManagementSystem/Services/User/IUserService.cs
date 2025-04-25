using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.User;

public interface IUserService
{
    Task<List<UserViewModel>> GetUsersAsync();
    Task<UserViewModel> GetUserByIdAsync(int id);
    Task<UserModel> CreateUserAsync(UserCreateViewModel userCreateViewModel); // Changed to UserCreateViewModel
    Task<bool> UpdateUserAsync(int id, UserViewModel userViewModel);
    Task<bool> DeleteUserAsync(int id);
    Task<(bool isSuccessful, string message)> LoginAsync(string identifier, string password);
    Task<bool> RoleExistsAsync(int roleId);
}
