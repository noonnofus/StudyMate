namespace ASPDotNetProject.Models.ViewModel
{
    public class ClassroomDetailViewModel
    {
        public Classroom Classroom { get; set; }
        public List<Messages> Messages { get; set; }
        public int UserId { get; set; }
        public bool IsUserJoined { get; set; }
    }
}
