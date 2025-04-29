using LeadManagementSystem.Models;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<salesperson>> GetAllAsync();
        Task<salesperson?> GetByIdAsync(int id);
    }
}
