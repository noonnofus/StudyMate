using System.Diagnostics;
using ASPDotNetProject.Models;
using ASPDotNetProject.Models.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace ASPDotNetProject.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUsersRepository _usersRepository;

        public HomeController(IUsersRepository usersRepository)
        {
            _usersRepository = usersRepository;
        }

        public ViewResult Index()
        {
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
            } else if (statusCode == 500) {
                return View("InternalError");
            }
            return View("Error");
        }
    }
}
