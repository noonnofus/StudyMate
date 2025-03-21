using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ASPDotNetProject.Models;
using Microsoft.AspNetCore.Identity;

namespace ASPDotNetProject.Data
{
    public class AppDBContext : IdentityDbContext<ApplicationUser, IdentityRole<Guid>, Guid>
    {
        public AppDBContext(DbContextOptions<AppDBContext> options)
            : base(options)
        {
        }

        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<ClassroomUser> ClassroomUser { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}
