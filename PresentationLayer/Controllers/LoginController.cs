using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DataAccessLayer.Models;
using PresentationLayer.ViewModel;
using System.Threading.Tasks;


namespace PresentationLayer.Controllers
{
    public class LoginController : Controller
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;

        public LoginController(SignInManager<ApplicationUser> signInManager, UserManager<ApplicationUser> userManager)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string? ReturnUrl = null)
        {
            Console.WriteLine($"Received returnUrl: '{ReturnUrl}'");
            // var externalLogins = ;
            LoginViewModel model = new LoginViewModel {
                ReturnUrl = ReturnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList(),
            };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Index", model);
            }

            var user = await _userManager.FindByEmailAsync(model.Email);
            Console.WriteLine($"User found: {user?.UserName}");

            if (user == null)
            {
                ModelState.AddModelError("", "Invalid email or password.");
                return View("Index", model);
            }

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, false, lockoutOnFailure: true);
            if (result.Succeeded)
            {
                HttpContext.Session.SetString("UserId", user.Id.ToString());
                HttpContext.Session.SetString("Username", user.UserName);
                HttpContext.Session.SetString("UserRole", user.Role);
                return RedirectToAction("Index", "Home");
            }
            else if (result.IsLockedOut)
            {
                ModelState.AddModelError("", "Your account has been locked due to multiple failed attempts. Try again later.");
            }
            else
            {
                ModelState.AddModelError("", "Invalid email or password.");
            }

            return View("Index", model);
        }


        [HttpPost]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Console.WriteLine(provider);
            // Console.WriteLine(returnUrl);
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }

        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();

            HttpContext.Session.Remove("UserId");
            HttpContext.Session.Remove("Username");
            
            HttpContext.Session.Clear();

            return RedirectToAction("Index", "Home");
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var existingUser = await _userManager.FindByEmailAsync(model.Email);
                if (existingUser != null)
                {
                    ModelState.AddModelError("Email", "This email is already in use.");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.UserName,
                    Email = model.Email,
                    Address = model.Address,
                };

                var result = await _userManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    HttpContext.Session.SetString("UserId", user.Id.ToString());
                    HttpContext.Session.SetString("Username", user.UserName);

                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View(model);
        }

    }
}
