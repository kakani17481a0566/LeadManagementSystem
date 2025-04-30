using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using LeadManagementSystem.Data;
using LeadManagementSystem.Models;
using LeadManagementSystem.ViewModel.Request;
using LeadManagementSystem.ViewModel.Response;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace LeadManagementSystem.Services.ServiceImpl
{
    public class SalesPersonServiceImpl : SalesPersonService
    {
        private static readonly Random _random = new Random();
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SalesPersonServiceImpl> _logger;

        public SalesPersonServiceImpl(
            ApplicationDbContext context,
            ILogger<SalesPersonServiceImpl> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<SalesPersonResponse> AddSalesPersonAsync(SalesPersonRequestVM request)
        {
            try
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
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding salesperson");
                throw;
            }
        }

        public async Task<IEnumerable<SalesPerson>> GetAllAsync()
        {
            try
            {
                return await _context.SalesPersons.AsNoTracking().ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all salespersons");
                throw;
            }
        }

        public async Task<SalesPerson?> GetByIdAsync(int id)
        {
            try
            {
                return await _context.SalesPersons.AsNoTracking()
                    .FirstOrDefaultAsync(sp => sp.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting salesperson by ID: {Id}", id);
                throw;
            }
        }

        public async Task<SalesPerson?> UpdateSalesPersonAsync(int id, SalesPersonVM request)
        {
            try
            {
                var salesPerson = await _context.SalesPersons
                    .FirstOrDefaultAsync(sp => sp.Id == id);

                if (salesPerson == null)
                    return null;

                salesPerson.Name = request.Name;
                salesPerson.Email = request.Email;
                salesPerson.PhoneNumber = request.PhoneNumber;
                salesPerson.PaymentType = request.PaymentType;
                salesPerson.FirstPayment = request.FirstPayment;
                salesPerson.RecurringPercentage = request.RecurringPercentage;

                await _context.SaveChangesAsync();
                return salesPerson;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating salesperson with ID: {Id}", id);
                throw;
            }
        }

        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                var person = await _context.SalesPersons.FindAsync(id);
                if (person == null)
                    return false;

                _context.SalesPersons.Remove(person);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting salesperson with ID: {Id}", id);
                throw;
            }
        }

        private static string GenerateRandomCode()
        {
            return _random.Next(100, 999).ToString(); // Generates 3-digit code
        }
    }
}