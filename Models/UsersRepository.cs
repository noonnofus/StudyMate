// using ASPDotNetProject.Data;
// using Microsoft.AspNetCore.Identity;
// using System.Collections.Generic;
// using System.Linq;

// namespace ASPDotNetProject.Models
// {
//     public class UsersRepository : IUsersRepository
//     {
//         private readonly AppDBContext _context;

//         public UsersRepository(AppDBContext context)
//         {
//             _context = context;
//         }

//         public ApplicationUser GetUserById(string id)
//         {
//             return _context.Users.FirstOrDefault(u => u.Id == id);
//         }

//         public IEnumerable<ApplicationUser> GetAllUsers()
//         {
//             return _context.Users.ToList();
//         }
//     }
// }
