using LeadManagementSystem.Models;

namespace LeadManagementSystem.ViewModel.Response
{
    public class SalesPersonResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public short PaymentType { get; set; }
        public decimal FirstPayment { get; set; }
        public decimal RecurringPercentage { get; set; }

        public static SalesPersonResponse ToViewModel(SalesPerson person) => new()
        {
            Id = person.Id,
            Name = person.Name,
            Code = person.Code,
            PhoneNumber = person.PhoneNumber,
            Email = person.Email,
            PaymentType = person.PaymentType,
            FirstPayment = person.FirstPayment,
            RecurringPercentage = person.RecurringPercentage
        };
    }
}