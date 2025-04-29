using System.Security.Cryptography.X509Certificates;
using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
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
        public SalesPersonResponse addSalesPerson(SalesPersonRequestVM request)
        {
            var Person = new salesperson()
            {
                Name = request.Name,
                Code = request.Name.Split(" ")[0] + GenerateRandomCode(),
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PaymentType = request.PaymentType,
                FirstPayment = request.FirstPayment,
                RecurringPercentage = request.RecurringPercentage

            };

            _context.SalesPersons.Add(Person);
            _context.SaveChanges();
            var response = SalesPersonResponse.ToViewModel(Person);            

            return response;
        
        }
        public static string GenerateRandomCode() 
        {
            var Chars = "0123456789";
            return new string(Chars.Select(c => Chars[new Random().Next(Chars.Length)]).Take(3).ToArray());

        }
    

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.SalesPersons.FindAsync(id);
            if (person == null) return false;

            _context.SalesPersons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public salespersonVM UpdateSalesPerson(int id, salespersonVM person)
        {
            var saleperson = _context.SalesPersons.FirstOrDefault(x => x.Id == id);
            if (saleperson != null)
            {
                saleperson.Name = person.Name;
                saleperson.Email = person.Email;
                saleperson.PhoneNumber = person.PhoneNumber;
                saleperson.PaymentType = person.PaymentType;
                saleperson.FirstPayment = person.FirstPayment;
                saleperson.RecurringPercentage = person.RecurringPercentage;

                _context.SaveChanges(); // EF will track the changes automatically
                return person;
            }

            return null;
        }



    }
}
