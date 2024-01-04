using System.Security.Claims;
using CVDataLayer;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers;

public class MeddelandeController : Controller
{
    private CVContext _dbContext;

    public MeddelandeController(CVContext dbContext)
    {
        _dbContext = dbContext;
    }

    public IActionResult VisaMeddelanden()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

        var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id).ToList();

        var vm = new MeddelandeViewModel
        {
            Meddelanden = messages
        };

        return View("Meddelande", vm);
    }

    public IActionResult Read(int id)
    {
        var message = _dbContext.Meddelande.FirstOrDefault(x => x.Id == id);
        if (message is not null)
        {
            message.Läst = true;
        }

        _dbContext.SaveChanges();

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

        var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id).ToList();

        var vm = new MeddelandeViewModel
        {
            Meddelanden = messages
        };

        return View("Meddelande", vm);
    }

    public IActionResult MeddelandeConfirm()
    {
        return View();
    }
}