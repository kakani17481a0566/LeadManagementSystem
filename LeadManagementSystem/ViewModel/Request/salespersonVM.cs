using System.ComponentModel.DataAnnotations;

namespace LeadManagementSystem.ViewModel.Request
{
    public class SalesPersonVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [MaxLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Phone]
        [StringLength(10, ErrorMessage = "Phone number must be 10 digits")]
        [RegularExpression(@"^[0-9]{10}$", ErrorMessage = "Phone number must contain only digits")]
        public string PhoneNumber { get; set; }


        [Range(1, 2)]
        public short PaymentType { get; set; }

        public decimal FirstPayment { get; set; }

        [Range(0, 100)]
        public decimal RecurringPercentage { get; set; }
    }
}