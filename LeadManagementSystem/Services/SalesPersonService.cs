using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<SalesPersonResponse> AddSalesPersonAsync(SalesPersonRequestVM request);
        Task<IEnumerable<SalesPerson>> GetAllAsync();
        Task<SalesPerson?> GetByIdAsync(int id);
        Task<SalesPerson?> UpdateSalesPersonAsync(int id, SalesPersonVM personVm);

        Task<bool> DeleteAsync(int id);
    }
}