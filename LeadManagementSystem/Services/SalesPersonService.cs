using LeadManagementSystem.Models;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<SalesPerson>> GetAllAsync();
        Task<SalesPerson?> GetByIdAsync(int id);
    }
}
