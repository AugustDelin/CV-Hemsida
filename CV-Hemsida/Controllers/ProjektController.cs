using CVDataLayer; // Importera CVDataLayer för att få tillgång till databasen
using CVModels; // Importera CVModels för att använda modellklasser
using CVModels.ViewModels; // Importera ViewModel för att hantera vyn för projekt
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasoperationer
using System.Linq; // Importera Linq för att hantera LINQ-frågor
using System.Security.Claims; // Importera Claims för att hantera användarinformation

namespace CV_Hemsida.Controllers // Namnet på din Controller
{
    public class ProjektController : BaseController // ProjektController ärver från BaseController för återanvändning av funktionalitet
    {
        private readonly CVContext _dbContext; // Databaskontexten för hantering av projekt

        public ProjektController(CVContext dbContext) : base(dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldela den inkommande databaskontexten till det privata fältet
        }

        // Visar en sida med en lista över projekt
        public IActionResult ProjectPage()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            var projekten = _dbContext.Projekts
               .Include(p => p.User) // Include the project creator
               .Select(p => new ProjektViewModel
               {
                   Id = p.Id,
                   Titel = p.Titel,
                   Beskrivning = p.Beskrivning,
                   Skapare = "Skapare: " + (p.User.UserName)
               })
               .ToList();

            return View(projekten); // Visa vyn för projektsidan med projekten
        }

        // Visar detaljer för ett specifikt projekt baserat på ID
        public IActionResult Details(int id)
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            var projekt = _dbContext.Projekts
                                    .Include(p => p.User) // Include the project creator
                                    .FirstOrDefault(p => p.Id == id);

            if (projekt == null)
            {
                return NotFound(); // Returnera 404 om projektet inte hittas
            }

            // Hämta en lista över deltagande användare i projektet
            var deltagandeAnvändare = _dbContext.Users
                .Join(
                    _dbContext.PersonDeltarProjekt,
                    u => u.Id,
                    pdp => pdp.Deltagare,
                    (u, pdp) => new { User = u, PersonDeltarProjekt = pdp }
                )
                .Where(j => j.PersonDeltarProjekt.Projekt == id)
                .Select(j => new AnvändareViewModel
                {
                    Namn = j.User.UserName ?? "Okänd Användare"
                })
                .ToList();

            var projektViewModel = new ProjektViewModel
            {
                Id = projekt.Id,
                Titel = projekt.Titel,
                Beskrivning = projekt.Beskrivning,
                Skapare = projekt.User?.UserName ?? "Okänd Skapare",
                DeltagandeAnvändare = deltagandeAnvändare
            };

            return View(projektViewModel); // Visa vyn med projektets detaljer och deltagande användare
        }

        // Skapar ett nytt projekt baserat på inmatad information
        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            // Kontrollera att modellen är giltig innan behandling
            if (ModelState.IsValid)
            {
                // Hämta ID för inloggad användare
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Skapa ett nytt projektobjekt
                var newProject = new Projekt
                {
                    Titel = model.Titel,
                    Beskrivning = model.Beskrivning,
                    AnvändarId = userId // Tilldela inloggad användare som projektets skapare
                };

                // Lägg till det nya projektet till databasen
                _dbContext.Projekts.Add(newProject);
                _dbContext.SaveChanges();

                // Omdirigera till Projektsidan efter att projektet har skapats
                return RedirectToAction("ProjectPage");
            }

            // Om modellen inte är giltig, returnera till skapandevyn med felmeddelanden
            return View(model);
        }

        // Sparar ändringar i ett befintligt projekt
        [HttpPost]
        public IActionResult SaveChanges(ChangeProjectViewModel model)
        {
            // Kontrollera att modellen är giltig innan behandling
            if (ModelState.IsValid)
            {
                var project = _dbContext.Projekts.FirstOrDefault(p => p.Id == model.Id);

                if (project == null)
                {
                    // Hantera fallet där projektet inte hittas
                    return RedirectToAction("ChangeProject", new { id = model.Id });
                }

                // Uppdatera projektets information med värdena från formuläret
                project.Titel = model.Titel;
                project.Beskrivning = model.Beskrivning;

                // Spara ändringar direkt till databasen
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Home"); // Omdirigera till en lämplig sida efter att ändringar har sparats
            }

            // Om modellen inte är giltig, returnera till ändringsvyn med valideringsfel
            return View("ChangeProject", model);
        }

        // Visar vyn för att ändra ett befintligt projekt baserat på ID
        [HttpGet]
        public IActionResult ChangeProject(int id)
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            var project = _dbContext.Projekts.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return RedirectToAction("Index", "Home"); // Omdirigera om projektet inte hittas
            }

            // Kontrollera om inloggad användare är skaparen av projektet
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (project.AnvändarId != userId)
            {
                // Användaren har inte behörighet att ändra projektet
                return RedirectToAction("ResourceNotFoundProject", "Projekt"); // Skapa en lämplig åtkomstnekat-vy
            }

            var viewModel = new ChangeProjectViewModel
            {
                Id = project.Id,
                Titel = project.Titel,
                Beskrivning = project.Beskrivning
            };

            return View(viewModel); // Visa vyn för att ändra projekt
        }

        // Visa vyn för att skapa ett nytt projekt
        public IActionResult CreateProject()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            return View();
        }

        // Visa vyn för att spara ändringar
        public IActionResult Save()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            return View();
        }

        // Visa vyn för att hantera resurs som inte hittades för projekt
        public IActionResult ResourceNotFoundProject()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            return View();
        }
    }
}
