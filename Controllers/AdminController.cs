using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ASPDotNetProject.Models;
using System.Threading.Tasks;

[Authorize(Roles = "Admin")]
public class AdminController : Controller
{
   private readonly UserManager<ApplicationUser> _userManager;

    public AdminController(UserManager<ApplicationUser> userManager)
    {
        _userManager = userManager;
    }

    public IActionResult Dashboard()
    {
        var users = _userManager.Users;
        return View(users);
    }

    public async Task<IActionResult> PromoteToAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        Console.WriteLine(user);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "Admin");
            user.Role = "Admin";
            await _userManager.UpdateAsync(user);
        }
        return RedirectToAction("Dashboard");
    }
    
    public async Task<IActionResult> DemoteAdmin(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            await _userManager.AddToRoleAsync(user, "User");
            user.Role = "User";
            await _userManager.UpdateAsync(user);
        }
        return RedirectToAction("Dashboard");
    }
    
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var user = await _userManager.FindByIdAsync(userId);
        if (user != null)
        {
            var result = await _userManager.DeleteAsync(user);
            if (result.Succeeded) {
            await _userManager.UpdateAsync(user);
            }
        }
        return RedirectToAction("Dashboard");
    }
}
