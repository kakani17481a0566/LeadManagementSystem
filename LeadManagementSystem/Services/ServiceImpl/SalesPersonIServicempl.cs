using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
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

        public async Task<IEnumerable<salesperson>> UpdateAllAsync()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<salesperson?> UpdateByIdAsync(int id)
        {
            return await _context.SalesPersons.FindAsync(id);
        }
        public salespersonVM UpdateSalesPerson(int id, salespersonVM person)
        {
            var saleperson = _context.SalesPersons.FirstOrDefault(x => x.Id == id);
            if (saleperson != null)
            {
                var updates = new salesPerson()
                {
                    Name = person.Name,
                    Email = person.Email,
                    PhoneNumber = person.PhoneNumber,
                    PaymentType=person.PaymentType,
                    FirstPayment = person.FirstPayment,
                    RecurringPercentage=person.RecurringPercentage               
                };
                _context.SaveChanges();
                return person;


            }
            return null;
        }

        internal async Task UpdateAll()
        {
            throw new NotImplementedException();
        }
    }
}
