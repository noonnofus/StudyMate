using DataAccessLayer.Models;

namespace PresentationLayer.ViewModel
{
    public class ClassroomViewModel
    {
        public IEnumerable<Classroom> Rooms { get; set; }
        public Guid UserId { get; set; }
        public string? PageTitle { get; set; }
        public Classroom Classroom { get; set; }
        public bool IsUserJoined { get; set; }
        public List<ApplicationUser> ClassroomUsers { get; set; } = new List<ApplicationUser>();
    }
}
