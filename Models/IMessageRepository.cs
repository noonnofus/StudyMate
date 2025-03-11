using System.Collections.Generic;
using ASPDotNetProject.Models;

namespace ASPDotNetProject.Repositories
{
    public interface IMessageRepository
    {
        IEnumerable<Messages> GetMessagesByClassroom(int classroomId);

        void AddMessage(Messages message);

        void SaveChanges();
    }
}
