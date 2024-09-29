using Microsoft.AspNetCore.Mvc;

public class DashboardController : Controller
{
    // Add the Authorize attribute to ensure only authenticated users can access this action
    [HttpGet]
    public IActionResult Index()
    {
        // Check if the user is logged in
        if (HttpContext.Session.GetString("Username") == null)
        {
            return RedirectToAction("Login", "Account");
        }

        return View();
    }
}
