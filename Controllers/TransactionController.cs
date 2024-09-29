using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Splitwise_clone.Models;
using System.Linq;
using System.Threading.Tasks;

public class TransactionController : Controller
{
    private readonly AppDbContext _context;

    public TransactionController(AppDbContext context)
    {
        _context = context;
    }

    // GET: Transaction/Create
    [HttpGet]
    public IActionResult Create()
    {
        ViewBag.Users = _context.Users.ToList();
        return View(new TransactionViewModel());
    }

    // POST: Transaction/Create
    [HttpPost]
    public async Task<IActionResult> Create(TransactionViewModel model)
    {
        if (ModelState.IsValid)
        {
            // Ensure the CreatedBy user exists
            var creator = await _context.Users.FindAsync(model.CreatedBy);
            if (creator == null)
            {
                ModelState.AddModelError("CreatedBy", "The creator does not exist.");
                ViewBag.Users = _context.Users.ToList();
                return View(model);
            }

            // Ensure all participants exist
            foreach (var participant in model.Participants)
            {
                var user = await _context.Users.FindAsync(participant.UserId);
                if (user == null)
                {
                    ModelState.AddModelError($"Participants[{model.Participants.IndexOf(participant)}].UserId", "One or more participants do not exist.");
                    ViewBag.Users = _context.Users.ToList();
                    return View(model);
                }
            }

            var transaction = new Transaction
            {
                Amount = model.Amount,
                Description = model.Description,
                Date = DateTime.Now,
                CreatedBy = model.CreatedBy, // Ensure this is set correctly
                Participants = model.Participants.Select(p => new Participant
                {
                    UserId = p.UserId,
                    PaidAmount = p.PaidAmount,
                    ShareAmount = Math.Round(model.Amount / model.Participants.Count, 2)
                }).ToList()
            };

            try
            {
                _context.Transactions.Add(transaction);
                await _context.SaveChangesAsync();
                Console.WriteLine("Transaction created successfully.");
                return RedirectToAction("Index", "Dashboard");
            }
            catch (DbUpdateException ex)
            {
               
                ModelState.AddModelError("", "Unable to save changes. Try again, and if the problem persists, see your system administrator.");
            }
        }

        ViewBag.Users = _context.Users.ToList();
        return View(model);
    }



     public async Task<IActionResult> Details(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Participants)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(t => t.TransactionId == id);

        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // GET: Transaction/List
    public async Task<IActionResult> List()
    {
        var transactions = await _context.Transactions
            .Include(t => t.Participants)
            .ThenInclude(p => p.User)
            .ToListAsync();

        return View(transactions);
    }

    // GET: Participant/List
    public async Task<IActionResult> Participants()
    {
        var participants = await _context.Participants
            .Include(p => p.User)
            .Include(p => p.Transaction)
            .ToListAsync();

        return View(participants);
    }

    // GET: Transaction/Balance
    public async Task<IActionResult> Balance()
    {
        var participants = await _context.Participants
            .Include(p => p.User)
            .Include(p => p.Transaction)
            .ToListAsync();

        var balanceDict = new Dictionary<string, double>();

        foreach (var participant in participants)
        {
            if (!balanceDict.ContainsKey(participant.User.UserName))
            {
                balanceDict[participant.User.UserName] = 0.0;
            }

            balanceDict[participant.User.UserName] += participant.PaidAmount - participant.ShareAmount;
        }

        ViewBag.Balance = balanceDict;
        return View();
    }

    // GET: Transaction/Delete/5
    [HttpGet]
    public async Task<IActionResult> Delete(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Participants)
            .ThenInclude(p => p.User)
            .FirstOrDefaultAsync(t => t.TransactionId == id);

        if (transaction == null)
        {
            return NotFound();
        }

        return View(transaction);
    }

    // POST: Transaction/Delete/5
    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var transaction = await _context.Transactions
            .Include(t => t.Participants)
            .FirstOrDefaultAsync(t => t.TransactionId == id);

        if (transaction == null)
        {
            return NotFound();
        }

        _context.Participants.RemoveRange(transaction.Participants);
        _context.Transactions.Remove(transaction);
        await _context.SaveChangesAsync();

        return RedirectToAction("List");
    }
    
}
