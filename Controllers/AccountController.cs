using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;
using System.Text;
using BCrypt.Net;
using Splitwise_clone.Models;
using System.Threading.Tasks;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Account/Signup
    [HttpGet]
    public IActionResult Signup()
    {
        return View();
    }

    // POST: Account/Signup
    [HttpPost]
    public async Task<IActionResult> Signup(SignupViewModel model)
    {
        if (ModelState.IsValid)
        {
            var existingUser = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);
            if (existingUser != null)
            {
                ModelState.AddModelError("Username", "Username already exists. Please choose a different username.");
                return View(model);
            }
            var hashedPassword = BCrypt.Net.BCrypt.HashPassword(model.Password);
            var user = new User
            {
                UserName = model.Username,
                Password = hashedPassword 
            };

            _context.Users.Add(user);
            await _context.SaveChangesAsync();

            // Sign in the user by setting a session variable
            HttpContext.Session.SetString("Username", user.UserName);

            // Redirect to the new protected page after successful signup
            return RedirectToAction("Index", "Dashboard");
        }

        return View(model);
    }

    // GET: Account/Login
    [HttpGet]
    public IActionResult Login()
    {
        return View();
    }

    // POST: Account/Login
    [HttpPost]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.UserName == model.Username);
            if (user != null && BCrypt.Net.BCrypt.Verify(model.Password, user.Password))
            {
                // Sign in the user by setting a session variable
                HttpContext.Session.SetString("Username", user.UserName);

                // Redirect to the new protected page after successful login
                return RedirectToAction("Index", "Dashboard");
            }
            else
            {
                ModelState.AddModelError("", "Invalid login attempt.");
            }
        }

        return View(model);
    }

    // POST: Account/Logout
    [HttpPost]
    public IActionResult Logout()
    {
        // Clear the session
        HttpContext.Session.Remove("Username");
        return RedirectToAction("Index", "Home");
    }

   // GET: Account/UserList
    public async Task<IActionResult> UserList()
    {
        var users = await _context.Users.ToListAsync();
        return View(users);
    }

    // GET: Account/DeleteUser/5
    [HttpGet]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return View(user);
    }

    // POST: Account/DeleteUser/5
    [HttpPost, ActionName("DeleteUser")]
    public async Task<IActionResult> DeleteUserConfirmed(int id)
    {
        var user = await _context.Users
            .Include(u => u.Participants)
            .ThenInclude(p => p.Transaction)
            .FirstOrDefaultAsync(u => u.UserId == id);

        if (user == null)
        {
            return NotFound();
        }

        var participants = user.Participants.ToList();
        foreach (var participant in participants)
        {
            _context.Participants.Remove(participant);
        }

        var transactions = await _context.Transactions
            .Where(t => t.CreatedBy == id)
            .ToListAsync();
        foreach (var transaction in transactions)
        {
            _context.Transactions.Remove(transaction);
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return RedirectToAction("UserList");
    }
}
