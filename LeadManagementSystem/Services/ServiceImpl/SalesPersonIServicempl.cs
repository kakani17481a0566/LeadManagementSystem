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

        public async Task<IEnumerable<salesperson>> GetAllAsync()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<salesperson?> GetByIdAsync(int id)
        {
            return await _context.SalesPersons.FindAsync(id);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.SalesPersons.FindAsync(id);
            if (person == null) return false;

            _context.SalesPersons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

    }
}
