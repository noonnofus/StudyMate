using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

namespace ASPDotNetProject.Controllers
{
    [Authorize]
    public class ProfileController : Controller
    {
        private readonly IClassRepository _roomsRepository;

        public ProfileController(IClassRepository roomsRepository)
        {
            _roomsRepository = roomsRepository;
        }

        public ViewResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");

            if (userId == null)
            {
                return View("~/Views/Home/Index.cshtml");
            }

            Guid parsedUserId = Guid.Parse(userId);

            var joinedRooms = _roomsRepository.GetUserClassrooms(parsedUserId);

            ClassroomViewModel classroomViewModel = new ClassroomViewModel()
            {
                UserId = parsedUserId,
                Rooms = joinedRooms,
            };

            return View(classroomViewModel);
        }
    }
}
