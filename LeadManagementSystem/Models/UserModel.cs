using System.ComponentModel.DataAnnotations.Schema;

namespace LeadManagementSystem.Models
{
    [Table("users")]  // Explicitly set the table name to lowercase 'users'
    public class UserModel
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("first_name")]
        public string FirstName { get; set; }

        [Column("last_name")]
        public string LastName { get; set; }

        [Column("email")]
        public string Email { get; set; }

        [Column("phone")]
        public string Phone { get; set; }

        [Column("login_id")]
        public string LoginId { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("role_id")]
        public int RoleId { get; set; }

        // Navigation property for the Role
        public virtual RolesModel Role { get; set; }
    }
}
