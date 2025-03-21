using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace ASPDotNetProject.Controllers
{
    [Authorize]
    public class CreateController : Controller
    {
        private readonly IClassRepository _roomsRepository;
        private readonly IClassroomUserRepository _classroomUserService;

        public CreateController(IClassRepository roomsRepository, IClassroomUserRepository classroomUserRepository)
        {
            _roomsRepository = roomsRepository;
            _classroomUserService = classroomUserRepository;
        }

        public ViewResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null) {
                return View("~/Views/Home/Index.cshtml");
            }

            ClassroomViewModel classroomViewModel = new ClassroomViewModel()
            {
                UserId = Guid.Parse(userId),
                Rooms = _roomsRepository.GetAllRooms(),
            };
            return View(classroomViewModel);
        }

        [HttpPost]
        public IActionResult CreateClass(string classname, string classtype) 
        {
            if (string.IsNullOrWhiteSpace(classname) || string.IsNullOrWhiteSpace(classtype))
            {
                return BadRequest("Class name and type cannot be empty.");
            }

            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return Unauthorized("You must be logged in to create a class.");
            }

            var newClassroom = new Classroom
            {
                ClassName = classname,
                Type = classtype,
            };
            _roomsRepository.AddRoom(newClassroom);

             var classroomUser = new ClassroomUser
            {
                ClassroomId = newClassroom.Id,
                UserId = Guid.Parse(userId)
            };
            _classroomUserService.AddClassroomUser(classroomUser);

            return RedirectToAction("Index", "Classroom");
        }

    }
}