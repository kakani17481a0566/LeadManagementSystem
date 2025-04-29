using LeadManagementSystem.Models;

using LeadManagementSystem.Data;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {

        Task<IEnumerable<salesperson>> GetAllAsync();
        Task<salesperson?> GetByIdAsync(int id);
        SalesPersonResponse addSalesPerson(SalesPersonRequestVM request);


        salespersonVM  UpdateSalesPerson(int id, salespersonVM person);

        Task<bool> DeleteAsync(int id);

    }
}
