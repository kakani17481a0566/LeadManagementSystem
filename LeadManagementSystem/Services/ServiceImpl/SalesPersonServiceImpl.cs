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
    // Service implementation for managing SalesPerson data
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

        // Add a new salesperson with a unique code
        public async Task<SalesPersonResponse> AddSalesPersonAsync(SalesPersonRequestVM request)
        {
            try
            {
                // Take first part of the name (limit to 47 characters if needed)
                var namePart = request.Name.Split(' ')[0];
                if (namePart.Length > 47)
                    namePart = namePart[..47];

                string code;

                // Generate unique code (namePart + random 3-digit number)
                do
                {
                    code = namePart + GenerateRandomCode();
                } while (await _context.SalesPersons.AnyAsync(sp => sp.Code == code));

                // Create new SalesPerson entity
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

                // Save to database
                _context.SalesPersons.Add(person);
                await _context.SaveChangesAsync();

                // Return response
                return SalesPersonResponse.ToViewModel(person);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error adding salesperson");
                throw;
            }
        }

        // Get all salespersons from the database
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

        // Get a single salesperson by ID
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

        // Update salesperson details
        public async Task<SalesPerson?> UpdateSalesPersonAsync(int id, SalesPersonVM request)
        {
            try
            {
                // Find salesperson by ID
                var salesPerson = await _context.SalesPersons
                    .FirstOrDefaultAsync(sp => sp.Id == id);

                if (salesPerson == null)
                    return null;

                // Update fields
                salesPerson.Name = request.Name;
                salesPerson.Email = request.Email;
                salesPerson.PhoneNumber = request.PhoneNumber;
                salesPerson.PaymentType = request.PaymentType;
                salesPerson.FirstPayment = request.FirstPayment;
                salesPerson.RecurringPercentage = request.RecurringPercentage;

                // Save changes
                await _context.SaveChangesAsync();
                return salesPerson;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating salesperson with ID: {Id}", id);
                throw;
            }
        }

        // Delete salesperson by ID
        public async Task<bool> DeleteAsync(int id)
        {
            try
            {
                // Find by ID
                var person = await _context.SalesPersons.FindAsync(id);
                if (person == null)
                    return false;

                // Remove from database
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

        // Generate a random 3-digit code
        private static string GenerateRandomCode()
        {
            return _random.Next(100, 999).ToString();
        }
    }
}
