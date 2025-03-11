namespace ASPDotNetProject.Models
{
    public interface IClassRepository
    {
        Classroom GetRoomByType(string type);
        Classroom GetRoomById(int Id);
        IEnumerable<Classroom> GetAllRooms();
        void UpdateRoom(Classroom updatedClassroom);
        void AddRoom(Classroom classroom);
        void DeleteRoom(int Id);
        ClassroomUser GetClassroomUser(int userId, int classroomId);
        void AddClassroomUser(ClassroomUser classroomUser);
        IEnumerable<Classroom> GetUserClassrooms(int userId);
    }
}
