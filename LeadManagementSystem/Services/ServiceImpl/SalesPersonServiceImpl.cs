using System;
using System.Threading.Tasks;
using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using Microsoft.EntityFrameworkCore;

namespace LeadManagementSystem.Services.ServiceImpl
{
    public class SalesPersonServiceImpl : SalesPersonService
    {
        private static readonly Random _random = new Random();
        private readonly ApplicationDbContext _context;

        public SalesPersonServiceImpl(ApplicationDbContext context)
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

        public async Task<SalesPersonResponse> AddSalesPersonAsync(SalesPersonRequestVM request)
        {
            var namePart = request.Name.Split(' ')[0];
            if (namePart.Length > 47)
                namePart = namePart[..47];

            string code;
            do
            {
                code = namePart + GenerateRandomCode();
            } while (await _context.SalesPersons.AnyAsync(sp => sp.Code == code));

            var person = new SalesPerson
            {
                Name = request.Name,
                Code = code,
                PhoneNumber = request.PhoneNumber,
                Email = request.Email,
                PaymentType = request.PaymentType,
                FirstPayment = request.FirstPayment,
                RecurringPercentage = request.RecurringPercentage
            };

            _context.SalesPersons.Add(person);
            await _context.SaveChangesAsync();
            return SalesPersonResponse.ToViewModel(person);
        }

        private static string GenerateRandomCode()
        {
            return _random.Next(100, 999).ToString(); // Generates 3-digit code
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var person = await _context.SalesPersons.FindAsync(id);
            if (person == null) return false;

            _context.SalesPersons.Remove(person);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<SalesPersonVM?> UpdateSalesPersonAsync(int id, SalesPersonVM personVm)
        {
            var salesPerson = await _context.SalesPersons.FirstOrDefaultAsync(sp => sp.Id == id);
            if (salesPerson == null) return null;

            salesPerson.Name = personVm.Name;
            salesPerson.Email = personVm.Email;
            salesPerson.PhoneNumber = personVm.PhoneNumber;
            salesPerson.PaymentType = personVm.PaymentType;
            salesPerson.FirstPayment = personVm.FirstPayment;
            salesPerson.RecurringPercentage = personVm.RecurringPercentage;

            await _context.SaveChangesAsync();
            return personVm;
        }
    }
}