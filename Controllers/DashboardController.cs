using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    [HttpGet]
    public IActionResult Index()
    {
        
        if (HttpContext.Session.GetString("Username") == null)
        {
            return RedirectToAction("Login", "Account");
        }

        var Username = HttpContext.Session.GetString("Username") ?? String.Empty;

        ViewBag.Username = Username;

        return View();
    }
}
