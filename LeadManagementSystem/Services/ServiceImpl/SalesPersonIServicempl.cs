using System.Security.Cryptography.X509Certificates;
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

        public async Task<IEnumerable<salesperson>> GetAllAsync()
        {
            return await _context.SalesPersons.ToListAsync();
        }

        public async Task<salesperson?> GetByIdAsync(int id)
        {
            return await _context.SalesPersons.FindAsync(id);
        }
        public SalesPersonRequestVM addSalesPerson(SalesPersonRequestVM request)
        {
            var Person = new SalesPerson()
            {
                Name = request.Name,
                Code = GenerateRandomCode(),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PaymentType = request.PaymentType,
                FirstPayment = request.FirstPayment,
                RecurringPercentage = request.RecurringPercentage

            };

            _context.SalesPersons.Add(Person);
            _context.SaveChanges();

            return request;
        
        }
        public static string GenerateRandomCode() 
        {
            var Chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Chars.Select(c => Chars[new Random().Next(Chars.Length)]).Take(5).ToArray());

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
