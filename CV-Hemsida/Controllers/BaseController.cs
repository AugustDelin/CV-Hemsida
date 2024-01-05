using CVDataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

public class BaseController : Controller
{
    private readonly CVContext _dbContext;

    public BaseController(CVContext dbContext)
    {
        _dbContext = dbContext;
    }
    public void SetMessageCount()
    {
        if (User.Identity.IsAuthenticated)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id && !x.Läst).ToList();

            ViewBag.Meddelanden = messages.Count;
        }
    }
}