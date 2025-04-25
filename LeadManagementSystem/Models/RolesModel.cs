using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("Roles")]  // Table Name for Roles
    public class RolesModel
    {
        [Column("id")]  // Column Name for Id
        public int Id { get; set; }

        [Column("role_name")]  // Column Name for Name
        public string Name { get; set; }

        // Navigation Property for Users (One-to-Many Relationship with UserModel)
        public ICollection<UserModel> Users { get; set; }
    }
}
