using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("salesperson")]
    public class SalesPerson
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        [Column("name")]
        public string Name { get; set; }

        [Required]
        [MaxLength(50)]
        [Column("code")]
        public string Code { get; set; }

        [MaxLength(20)]
        [Column("phonenumber")]
        [Phone]
        public string PhoneNumber { get; set; }

        [MaxLength(100)]
        [Column("email")]
        [EmailAddress]
        public string Email { get; set; }

        [Range(1, 2)]
        [Column("paymenttype")]
        public short PaymentType { get; set; }

        [Column("firstpayment", TypeName = "decimal(10,2)")]
        public decimal FirstPayment { get; set; }

        [Column("recurringpercentage", TypeName = "decimal(5,2)")]
        public decimal RecurringPercentage { get; set; }

        public ICollection<LeadEntity> Leads { get; set; }
    }
}