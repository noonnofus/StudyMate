namespace ASPDotNetProject.Models.ViewModel
{
    public class ClassroomViewModel
    {
        public IEnumerable<Classroom> Rooms { get; set; }
        public int UserId { get; set; }
        public string? PageTitle { get; set; }
        public Classroom Classroom { get; set; }
        public bool IsUserJoined { get; set; }
        public List<Users> ClassroomUsers { get; set; } = new List<Users>();
    }
}
