using LeadManagementSystem.Models;

using LeadManagementSystem.Data;
using LeadManagementSystem.ViewModel.Request;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<salesperson>> GetAllAsync();
        Task<salesperson?> GetByIdAsync(int id);
      
        SalesPersonRequestVM addSalesPerson(SalesPersonRequestVM request);


        
       


        Task<bool> DeleteAsync(int id);

    }
}
