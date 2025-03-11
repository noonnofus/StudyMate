using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Repositories;
using Google.Protobuf;

namespace ASPDotNetProject.Controllers
{
    public class ClassroomDetailController : Controller
    {
        private readonly IClassRepository _roomsRepository;
        private readonly IUsersRepository _usersRepository;
        private readonly IClassroomUserRepository _classroomUserService;
        private readonly IMessageRepository _messageRepository;

        public ClassroomDetailController(IClassRepository roomsRepository, IUsersRepository userRepository, IClassroomUserRepository classroomUserRepository, IMessageRepository messageRepository)
        {
            _roomsRepository = roomsRepository;
            _usersRepository = userRepository;
            _classroomUserService = classroomUserRepository;
            _messageRepository = messageRepository;
        }

        public IActionResult Index(int id)
        {
            var classroom = _roomsRepository.GetRoomById(id);

            if (classroom == null)
            {
                return NotFound("Classroom not found");
            }

            var messages = _messageRepository.GetMessagesByClassroom(id).ToList();

            var currentUserId = HttpContext.Session.GetString("UserId");

            if (currentUserId == null) {
                return View("~/Views/Home/Index.cshtml");
            }

            var isUserJoined = _classroomUserService.GetClassroomUser(id, int.Parse(currentUserId)) != null;

            var viewModel = new ClassroomDetailViewModel
            {
                Classroom = classroom,
                Messages = messages,
                UserId = int.Parse(currentUserId),
                IsUserJoined = isUserJoined
            };

            return View(viewModel);
        }

        public IActionResult JoinClass(int id)
        {
            var classroom = _roomsRepository.GetRoomById(id);

            if (classroom == null)
            {
                return NotFound("Classroom not found");
            }

            var currentUserId = HttpContext.Session.GetString("UserId");

            if (currentUserId == null) {
                return View("~/Views/Home/Index.cshtml");
            }

            if (_classroomUserService.GetClassroomUser(id, int.Parse(currentUserId)) == null)
            {
                var classroomUser = new ClassroomUser
                {
                    ClassroomId = id,
                    UserId = int.Parse(currentUserId)
                };
                _classroomUserService.AddClassroomUser(classroomUser);
            }

            return RedirectToAction("Index", new { id });
        }

        [HttpPost]
        public IActionResult SendMessage(int classroomId, string messageContent)
        {
            var classroom = _roomsRepository.GetRoomById(classroomId);
            if (classroom == null)
            {
                return NotFound("Classroom not found");
            }
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (string.IsNullOrWhiteSpace(messageContent))
            {
                return BadRequest("Message cannot be empty");
            }

            var newMessage = new Messages
            {
                UserId = int.Parse(currentUserId),
                ClassroomId = classroomId,
                Content = messageContent,
                Timestamp = DateTime.UtcNow
            };

            _messageRepository.AddMessage(newMessage);

            return RedirectToAction("Index", new { id = classroomId });
        }
    }
}
