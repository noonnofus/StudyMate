using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Repositories;
using System.Linq;

namespace ASPDotNetProject.Controllers
{
    public class DeleteClassController : Controller
    {
        private readonly IClassRepository _classroomService;
        private readonly IClassroomUserRepository _classroomUserService;

        public DeleteClassController(IClassRepository classroomService, IClassroomUserRepository classroomUserRepository)
        {
            _classroomService = classroomService;
            _classroomUserService = classroomUserRepository;
        }

        public IActionResult Index(int id)
        {
            var classroom = _classroomService.GetRoomById(id);
            if (classroom == null)
            {
                return NotFound();
            }

            var viewModel = new ClassroomViewModel
            {
                Classroom = classroom
            };

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult DeleteClass(int id)
        {
            if (id <= 0)
            {
                return BadRequest("Invalid classroom ID");
            }

            var classroom = _classroomService.GetRoomById(id);
            if (classroom == null)
            {
                return NotFound("Classroom not found");
            }

            var currentUserId = HttpContext.Session.GetString("UserId");

            if (currentUserId == null) {
                return View("~/Views/Home/Index.cshtml");
            }

             var classroomUser = _classroomUserService.GetClassroomUser(classroom.Id, int.Parse(currentUserId));
            if (classroomUser != null)
            {
                _classroomUserService.DeleteClassroomUser(classroomUser);
            }


            var remainingUsers = _classroomUserService.GetClassroomUsersByClassroomId(classroom.Id);
            if (!remainingUsers.Any())
            {
                _classroomService.DeleteRoom(id);
            }


            return RedirectToAction("Index", "Profile");
        }
    }
}
