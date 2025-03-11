using MVCLesson5.Model;

namespace ASPDotNetProject.Models {
    public class UsersRepository : IUsersRepository
    {
        private readonly AppDBContext _context;

        public UsersRepository(AppDBContext context)
        {
            _context = context;
        }

        public Users GetUserById(int id)
        {
            return _context.Users.FirstOrDefault(u => u.Id == id);
        }

        public IEnumerable<Users> GetAllUsers()
        {
            return _context.Users.ToList();
        }
    }
}
