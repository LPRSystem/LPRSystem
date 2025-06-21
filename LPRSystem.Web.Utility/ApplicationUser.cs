namespace LPRSystem.Web.Utility
{
    public class ApplicationUser
    {
        public long? Id { get; set; }
        public string FullName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public long? RoleId { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
