using PresentationLayer.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Models;
using BusinessLayer.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace PresentationLayer.Controllers
{
    [Authorize]
    public class ClassroomDetailController : Controller
    {
        private readonly IClassRepository _roomsRepository;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly IClassroomUserRepository _classroomUserService;
        private readonly IMessageRepository _messageRepository;

        public ClassroomDetailController(
            IClassRepository roomsRepository,
            UserManager<ApplicationUser> userManager,
            IClassroomUserRepository classroomUserRepository,
            IMessageRepository messageRepository)
        {
            _roomsRepository = roomsRepository;
            _userManager = userManager;
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

            var isUserJoined = _classroomUserService.GetClassroomUser(id, Guid.Parse(currentUserId)) != null;

            var viewModel = new ClassroomDetailViewModel
            {
                Classroom = classroom,
                Messages = messages,
                UserId = Guid.Parse(currentUserId),
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

            if (_classroomUserService.GetClassroomUser(id, Guid.Parse(currentUserId)) == null)
            {
                var classroomUser = new ClassroomUser
                {
                    ClassroomId = id,
                    UserId = Guid.Parse(currentUserId)
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
                UserId = Guid.Parse(currentUserId),
                ClassroomId = classroomId,
                Content = messageContent,
                Timestamp = DateTime.UtcNow
            };

            _messageRepository.AddMessage(newMessage);

            return RedirectToAction("Index", new { id = classroomId });
        }

    }
}
