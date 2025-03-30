using System.Collections.Generic;
using DataAccessLayer.Models;

namespace BusinessLayer.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<Messages> GetMessagesByClassroom(int classroomId);

        void AddMessage(Messages message);

        void SaveChanges();
    }
}
