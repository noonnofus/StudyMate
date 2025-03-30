using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLayer.Repositories
{
    public interface IClassroomUserRepository
    {
        ClassroomUser GetClassroomUser(int classroomId, Guid userId);
        IEnumerable<ClassroomUser> GetClassroomUsersByClassroomId(int classroomId);
        void AddClassroomUser(ClassroomUser classroomUser);
        void DeleteClassroomUser(ClassroomUser classroomUser);
    }
}
