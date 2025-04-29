using LeadManagementSystem.Models;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<salesperson>> UpdateAllAsync();
        Task<salesperson?> UpdateByIdAsync(int id);
    }
}
