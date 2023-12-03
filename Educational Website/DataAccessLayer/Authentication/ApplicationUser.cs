using Microsoft.AspNetCore.Identity;
namespace DataAccessLayer.Authentication
{
    public class ApplicationUser : IdentityUser
    {
        public override string? UserName { get; set; }
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime? BirthDate { get; set; }
        public string? Address { get; set; }
        public string? Gender { get; set; }
        public string? Role { get; set; }
    }
}
