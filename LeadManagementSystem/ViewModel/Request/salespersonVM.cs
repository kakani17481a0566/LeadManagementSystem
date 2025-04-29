using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.ViewModel.Request
{
    public class salespersonVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }
        [MaxLength(50)]
        public string Email { get; set; }
        
        
        public string PhoneNumber { get; set; }
        public short PaymentType { get; set; }
        public decimal FirstPayment { get; set; }
        public decimal RecurringPercentage { get; set; }

    }
}