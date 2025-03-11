namespace ASPDotNetProject.Models
{
    public interface IUsersRepository
    {
        Users GetUserById(int id);
        IEnumerable<Users> GetAllUsers();
    }
}