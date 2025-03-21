using System.Collections.Generic;
using System.Linq;
using ASPDotNetProject.Models;
using Microsoft.EntityFrameworkCore;
using ASPDotNetProject.Data;

namespace ASPDotNetProject.Repositories
{
    public class MessageRepository : IMessageRepository
    {
        private readonly AppDBContext _context;

        public MessageRepository(AppDBContext context)
        {
            _context = context;
        }

        public IEnumerable<Messages> GetMessagesByClassroom(int classroomId)
        {
            return _context.Messages
                .Where(m => m.ClassroomId == classroomId)
                .Include(m => m.User)
                .OrderBy(m => m.Timestamp)
                .ToList();
        }

        public void AddMessage(Messages message)
        {
            _context.Messages.Add(message);
            SaveChanges();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}
