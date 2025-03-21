using ASPDotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using ASPDotNetProject.Data;

namespace ASPDotNetProject.Repositories
{
    public class ClassRepository : IClassRepository
    {
        private readonly AppDBContext _context;

        public ClassRepository(AppDBContext context)
        {
            _context = context;
        }

        public Classroom GetRoomByType(string type)
        {
            return _context.Classroom
                           .FirstOrDefault(c => c.Type.Equals(type, StringComparison.OrdinalIgnoreCase));
        }

        public Classroom GetRoomById(int id)
        {
            return _context.Classroom
                           .FirstOrDefault(c => c.Id == id);
        }

        public IEnumerable<Classroom> GetAllRooms()
        {
            var result = _context.Classroom
                .Include(c => c.ClassroomUsers)
                .ToList();
            return result;
        }

        public void UpdateRoom(Classroom updatedClassroom)
        {
            _context.Classroom.Update(updatedClassroom);
            _context.SaveChanges();
        }

        public void AddRoom(Classroom classroom)
        {
            _context.Classroom.Add(classroom);
            _context.SaveChanges();
        }

        public void DeleteRoom(int id)
        {
            var classroom = _context.Classroom.FirstOrDefault(c => c.Id == id);
            if (classroom != null)
            {
                _context.Classroom.Remove(classroom);
                _context.SaveChanges();
            }
        }

        public ClassroomUser GetClassroomUser(Guid userId, int classroomId)
        {
            return _context.ClassroomUser
                           .FirstOrDefault(cu => cu.UserId == userId && cu.ClassroomId == classroomId);
        }

        public void AddClassroomUser(ClassroomUser classroomUser)
        {
            _context.ClassroomUser.Add(classroomUser);
            _context.SaveChanges();
        }

        public IEnumerable<Classroom> GetUserClassrooms(Guid userId)
        {
            return _context.Classroom
                .Where(c => c.ClassroomUsers.Any(cu => cu.UserId == userId))
                .Include(c => c.ClassroomUsers)
                .ToList();
        }
    }
}
