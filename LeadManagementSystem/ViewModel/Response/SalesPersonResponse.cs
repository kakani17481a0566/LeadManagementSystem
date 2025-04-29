using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LeadManagementSystem.Models;
using Optivem.Framework.Core.Application;

namespace LeadManagementSystem.ViewModel.Response
{
    public class SalesPersonResponse
    {

        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public short PaymentType { get; set; }
        public decimal FirstPayment { get; set; }
        public decimal RecurringPercentage { get; set; }
        
        public static SalesPersonResponse ToViewModel(salesperson salesperson)
        {
            var result = new SalesPersonResponse()
            {
                Name = salesperson.Name,
                Code = salesperson.Code,
                PhoneNumber = salesperson.PhoneNumber,
                Email = salesperson.Email,
                PaymentType = salesperson.PaymentType,
                FirstPayment = salesperson.FirstPayment,
                RecurringPercentage = salesperson.RecurringPercentage

            };
            return result;

        }
    }
}
