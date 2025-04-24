namespace LeadManagementSystem.ViewModel.Role
{
    public class RoleViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        // Optional: If you need to show associated users in the role view
        public List<string> Users { get; set; }  // A list of usernames or user IDs
    }
}
