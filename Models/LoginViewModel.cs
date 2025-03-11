using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace ASPDotNetProject.Models
{
    public class LoginViewModel
    {
         [Key]
        public int userId { get; set; }

        [Required(ErrorMessage = "Username is requried!")]
        public string username { get; set; }

        [Required(ErrorMessage = "Password is requried!")]
        public string password { get; set; }
    }
}
