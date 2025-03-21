using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASPDotNetProject.Controllers
{
    [Authorize]
    public class ClassroomController : Controller
    {
        private readonly IClassRepository _roomsRepository;

        public ClassroomController(IClassRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }
        [HttpPost]
        public IActionResult JoinClass(int classroomId)
        {
            var classroom = _roomsRepository.GetRoomById(classroomId);

            if (classroom == null)
            {
                return NotFound("Classroom not found");
            }
            
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (currentUserId == null)
            {
                return Unauthorized("You must be logged in to create a class.");
            }

            Guid userId = Guid.Parse(currentUserId);

            var existingEntry = _roomsRepository.GetClassroomUser(userId, classroomId);

            if (existingEntry == null)
            {
                var newClassroomUser = new ClassroomUser
                {
                    UserId = userId,
                    ClassroomId = classroomId
                };

                _roomsRepository.AddClassroomUser(newClassroomUser);
            }

            return RedirectToAction("Index");
        }
        public ViewResult Index()
        {
            var currentUserId = HttpContext.Session.GetString("UserId");

            if (currentUserId == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }

            ClassroomViewModel classroomViewModel = new ClassroomViewModel()
            {
                UserId = Guid.Parse(currentUserId),
                Rooms = _roomsRepository.GetAllRooms(),
            };
            return View(classroomViewModel);
        }
    }
}
