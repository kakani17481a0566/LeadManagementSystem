using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace LeadManagementSystem.ViewModel.Request
{
    public class SalesPersonRequestVM
    {
        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        //[MaxLength(50)]
        //public string Code { get; set; }

        [MaxLength(20)]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        public string Email { get; set; }

        [Range(1, 2)]
        public short PaymentType { get; set; }

        [Column(TypeName = "decimal(10,2)")]
        public decimal FirstPayment { get; set; }

        [Column(TypeName = "decimal(5,2)")]
        public decimal RecurringPercentage { get; set; }

    }
}
