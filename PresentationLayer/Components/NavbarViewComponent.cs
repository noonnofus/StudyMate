using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

public class NavbarViewComponent : ViewComponent
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public NavbarViewComponent(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public IViewComponentResult Invoke()
    {
        var username = _httpContextAccessor.HttpContext.Session.GetString("Username");
        var role = _httpContextAccessor.HttpContext.Session.GetString("UserRole");
        return View("Default", (username, role));
    }
}
