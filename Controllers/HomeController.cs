using Microsoft.AspNetCore.Identity;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Models;  // ApplicationUser 추가

namespace ASPDotNetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public HomeController(UserManager<ApplicationUser> userManager)
        {
            _userManager = userManager;
        }

        public ViewResult Index()
        {
            var userId = HttpContext.Session.GetString("UserId");
            Console.WriteLine($"UserId in session: {userId}");
            HomeIndexViewModel homeIndexViewModel = new HomeIndexViewModel()
            {
                PageTitle = "",
            };
            return View(homeIndexViewModel);
        }

        public IActionResult Error()
        {
            var statusCode = HttpContext.Response.StatusCode;
            if (statusCode == 404)
            {
                return View("NotFound");
            }
            else if (statusCode == 500)
            {
                return View("InternalError");
            }
            return View("Error");
        }
    }
}
