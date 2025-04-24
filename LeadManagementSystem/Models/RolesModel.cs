using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("Roles")]
    public class RolesModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("role_name")]
        public string Name { get; set; }

        public ICollection<UserModel> Users { get; set; }
    }
}
