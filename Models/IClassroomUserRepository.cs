using ASPDotNetProject.Models;
using System.Collections.Generic;

namespace ASPDotNetProject.Repositories
{
    public interface IClassroomUserRepository
    {
        ClassroomUser GetClassroomUser(int classroomId, Guid userId);
        IEnumerable<ClassroomUser> GetClassroomUsersByClassroomId(int classroomId);
        void AddClassroomUser(ClassroomUser classroomUser);
        void DeleteClassroomUser(ClassroomUser classroomUser);
    }
}
