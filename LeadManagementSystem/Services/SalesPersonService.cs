using LeadManagementSystem.Models;

using LeadManagementSystem.Data;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<salesperson>> UpdateAllAsync();
        Task<salesperson?> UpdateByIdAsync(int id);
        Task<IEnumerable<salesperson>> GetAllAsync();
        Task<salesperson?> GetByIdAsync(int id);
        SalesPersonResponse addSalesPerson(SalesPersonRequestVM request);


        Task<bool> DeleteAsync(int id);

    }
}
