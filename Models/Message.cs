using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ASPDotNetProject.Models
{
    public class Messages
    {
        public int Id { get; set; }

        public Guid UserId { get; set; } 

        public int ClassroomId { get; set; }

        [Required]
        [MaxLength(1000)]
        public string Content { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [ForeignKey("UserId")]
        public virtual ApplicationUser User { get; set; }
        
        [ForeignKey("ClassroomId")]
        public virtual Classroom Classroom { get; set; }
    }
}
