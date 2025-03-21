using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace ASPDotNetProject.Models
{
    public class ApplicationUser : IdentityUser<Guid>
    {
        public string? Address { get; set; }

        [Required]
        public string Role { get; set; } = "User";

        public ICollection<ClassroomUser> ClassroomUsers { get; set; }
    }
}
