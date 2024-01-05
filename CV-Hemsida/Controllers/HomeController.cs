using CVDataLayer; // Importera dataåtkomstlagret
using CVModels; // Importera CV-relaterade modeller
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet
using CVModels.ViewModels; // Importera CV-relaterade view-modeller
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasåtkomst
using System.Linq; // Importera LINQ-uttryck för databashantering
using System.Security.Claims; // Importera Claims för användarinformation

namespace CV_Hemsida.Controllers // Namnet på din Controller
{
    public class HomeController : Controller // HomeController ärver från Controller-basen
    {
        private CVContext _dbContext; // Databaskontexten för CV

        public HomeController(CVContext dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldela den inkommande databaskontexten till det privata fältet
        }

        public IActionResult Index() // Hanterar vyn för startsidan
        {
            if (User.Identity.IsAuthenticated) // Kontrollera om användaren är inloggad
            {
                // Hämta användar-ID för inloggad användare
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Hämta användarobjektet från databasen baserat på användar-ID
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                // Hämta meddelanden för användaren som inte har lästs och räkna antalet
                var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id && !x.Läst).ToList();

                // Skicka antalet olästa meddelanden till vyn
                ViewBag.Meddelanden = messages.Count;
            }

            // Hämta CVs som inte är markerade som privata och skapa CVViewModel-objekt för varje CV
            var cvViewModels = _dbContext.CVs
                .Where(cv => !cv.User.Privat)
                .Select(cv => new CVViewModel
                {
                    Id = cv.Id,
                    AnvändarId = cv.User.Id,
                    AnvändarNamn = cv.User.UserName,
                    Kompetenser = cv.Kompetenser,
                    Utbildningar = cv.Utbildningar,
                    TidigareErfarenhet = cv.TidigareErfarenhet
                })
                .ToList();

            // Hämta det senaste projektet
            var latestProject = _dbContext.Projekts
                .OrderByDescending(p => p.Id)
                .Select(p => new ProjektViewModel
                {
                    Id = p.Id,
                    Titel = p.Titel,
                    Beskrivning = p.Beskrivning
                })
                .FirstOrDefault();

            // Skapa en CombinedViewModel instans
            var viewModel = new CombinedViewModel
            {
                CVs = cvViewModels,
                StartPage = new StartPageViewModel
                {
                    LatestProject = latestProject
                }
            };

            // Skicka CombinedViewModel till vyn
            return View(viewModel);
        }


    }
}