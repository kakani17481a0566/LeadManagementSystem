namespace LeadManagementSystem.ViewModel.User
{
    public class UserViewModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string LoginId { get; set; }

        // Add Password field here
        public string Password { get; set; }

        public int RoleId { get; set; }
        public string RoleName { get; set; }  // Assuming RoleName is in the Role model
    }
}
