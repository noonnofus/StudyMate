using ASPDotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using MVCLesson5.Model;

namespace ASPDotNetProject.Repositories
{
    public class ClassroomUserRepository : IClassroomUserRepository
    {
        private readonly AppDBContext _context;

        public ClassroomUserRepository(AppDBContext context)
        {
            _context = context;
        }

        public ClassroomUser GetClassroomUser(int classroomId, int userId)
        {
            return _context.ClassroomUser
                           .FirstOrDefault(cu => cu.ClassroomId == classroomId && cu.UserId == userId);
        }

        public IEnumerable<ClassroomUser> GetClassroomUsersByClassroomId(int classroomId)
        {
            return _context.ClassroomUser
                           .Where(cu => cu.ClassroomId == classroomId)
                           .ToList();
        }

        public void AddClassroomUser(ClassroomUser classroomUser)
        {
            _context.ClassroomUser.Add(classroomUser);
            _context.SaveChanges();
        }

        public void DeleteClassroomUser(ClassroomUser classroomUser)
        {
            _context.ClassroomUser.Remove(classroomUser);
            _context.SaveChanges();
        }
    }
}
