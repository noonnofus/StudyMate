using DataAccessLayer.Models;

namespace PresentationLayer.ViewModel
{
    public class ClassroomDetailViewModel
    {
        public Classroom Classroom { get; set; }
        public List<Messages> Messages { get; set; }

        public Guid UserId { get; set; } 

        public bool IsUserJoined { get; set; }
    }
}
