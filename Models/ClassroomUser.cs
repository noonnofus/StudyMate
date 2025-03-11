using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using ASPDotNetProject.Models;
using System.ComponentModel.DataAnnotations.Schema;

public class ClassroomUser
{
    [Key]
    public int Id { get; set; }  
    public int ClassroomId { get; set; }
    public int UserId { get; set; }

    public Users User { get; set; }
    public Classroom Classroom { get; set; } 
}

