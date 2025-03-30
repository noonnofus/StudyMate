using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using DataAccessLayer.Models;

namespace DataAccessLayer.Models
{
   public class Classroom
    {
        public int Id { get; set; }
        public string ClassName { get; set; }
        public string Type { get; set; }

        public List<Messages> Messages { get; set; }
        public IEnumerable<ClassroomUser> ClassroomUsers { get; set; }
    }
}

