using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using Microsoft.EntityFrameworkCore;

namespace LeadManagementSystem.Services.ServiceImpl
{
    public class SalesPersonIServicempl : SalesPersonService
    {
        private readonly ApplicationDbContext _context;

        public SalesPersonIServicempl(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<SalesPerson>> GetAllAsync()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<SalesPerson?> GetByIdAsync(int id)
        {
            return await _context.SalesPersons.FindAsync(id);
        }
    }
}
