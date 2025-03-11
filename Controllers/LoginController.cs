using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Models;
using System.Linq;

namespace ASPDotNetProject.Controllers
{
    public class LoginController : Controller
    {
        private readonly IUsersRepository _userRepository;

        public LoginController(IUsersRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public ViewResult Login()
        {
            return View("Index");
        }

        [HttpPost]
        public IActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = _userRepository.GetAllUsers()
                    .FirstOrDefault(u => u.Username == model.username && u.Password == model.password);

                if (user != null)
                {
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Username", user.Username);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError("", "Invalid username or password");
            }

            return View("Index", model);
        }

        [HttpPost]
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
