using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DataAccessLayer.Models
{
    public class ClassroomUser
    {
        [Key]
        public int Id { get; set; }  

        public int ClassroomId { get; set; }
        
        [ForeignKey("User")]
        public Guid UserId { get; set; }

        public ApplicationUser User { get; set; }
        public Classroom Classroom { get; set; } 
    }

}
