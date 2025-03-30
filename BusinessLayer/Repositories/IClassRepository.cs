using DataAccessLayer.Models;

namespace BusinessLayer.Repositories
{
    public interface IClassRepository
    {
        Classroom GetRoomByType(string type);
        Classroom GetRoomById(int Id);
        IEnumerable<Classroom> GetAllRooms();
        void UpdateRoom(Classroom updatedClassroom);
        void AddRoom(Classroom classroom);
        void DeleteRoom(int Id);
        ClassroomUser GetClassroomUser(Guid userId, int classroomId);
        void AddClassroomUser(ClassroomUser classroomUser);
        IEnumerable<Classroom> GetUserClassrooms(Guid userId);
    }
}
