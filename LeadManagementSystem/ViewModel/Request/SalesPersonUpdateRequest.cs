using System.ComponentModel.DataAnnotations;

namespace LeadManagementSystem.ViewModel.Request
{
    public record SalesPersonUpdateRequest
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; init; }

        [EmailAddress]
        [MaxLength(100)]
        public string Email { get; init; }

        [Required]
        [StringLength(10, MinimumLength = 10)]
        [RegularExpression(@"^[0-9]+$")]
        public string PhoneNumber { get; init; }

        [Range(1, 2)]
        public short PaymentType { get; init; }

        public decimal FirstPayment { get; init; }

        [Range(0, 100)]
        public decimal RecurringPercentage { get; init; }
    }
}
