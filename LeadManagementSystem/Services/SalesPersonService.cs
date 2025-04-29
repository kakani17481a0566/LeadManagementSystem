// LeadManagementSystem/Services/SalesPersonService.cs
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace LeadManagementSystem.Services
{
    public interface SalesPersonService
    {
        Task<IEnumerable<SalesPerson>> GetAllAsync();
        Task<SalesPerson?> GetByIdAsync(int id);
        Task<SalesPersonResponse> AddSalesPersonAsync(SalesPersonRequestVM request);
        Task<bool> DeleteAsync(int id);
        Task<SalesPersonVM?> UpdateSalesPersonAsync(int id, SalesPersonVM person);
    }
}