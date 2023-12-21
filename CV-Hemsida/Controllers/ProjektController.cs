using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CV_Hemsida.Controllers
{
    public class ProjektController : Controller
    {
        private readonly CVContext _dbContext;

        public ProjektController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult ProjectPage()
        {
            var projekten = _dbContext.Projekts
               .Select(p => new ProjektViewModel
               {
                   Id = p.Id,
                   Titel = p.Titel,
                   Beskrivning = p.Beskrivning
               })
               .ToList();

            return View(projekten);
        }

        public IActionResult Details(int id)
        {
            var projekt = _dbContext.Projekts
                                    .Include(p => p.User) // Inkludera projektets skapare
                                    .FirstOrDefault(p => p.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            // Hämta en lista av användare som är kopplade till detta projekt på något sätt.
            var deltagandeAnvändare = _dbContext.Users
                                               .Where(u => u.SkapadeProjekt.Any(p => p.Id == id))
                                               .ToList();

            // Konvertera till AnvändareViewModel
            var deltagandeAnvändareViewModels = deltagandeAnvändare.Select(u => new AnvändareViewModel
            {
                Namn = u.UserName ?? "Okänd Användare" // Hantera nullvärden
            }).ToList();

            var projektViewModel = new ProjektViewModel
            {
                Id = projekt.Id,
                Titel = projekt.Titel,
                Beskrivning = projekt.Beskrivning,
                Skapare = projekt.User?.UserName ?? "Okänd Skapare", // Hantera nullvärden
                DeltagandeAnvändare = deltagandeAnvändareViewModels // Använd den konverterade listan här
            };

            return View(projektViewModel);
        }
    }
}
