using Microsoft.EntityFrameworkCore;
using ASPDotNetProject.Models;

namespace MVCLesson5.Model
{
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
            
        }

        public DbSet<Classroom> Classroom { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<ClassroomUser> ClassroomUser { get; set; }
        public DbSet<Messages> Messages { get; set; }
    }
}