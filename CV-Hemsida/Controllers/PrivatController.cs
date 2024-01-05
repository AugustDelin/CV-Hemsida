using CVDataLayer; // Importera CVDataLayer för databasåtkomst
using CVModels; // Importera CVModels för modellklasser
using CVModels.ViewModels; // Importera ViewModel för privat profil
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasoperationer
using System.Security.Claims; // Importera Claims för användarinformation

namespace CV_Hemsida.Controllers // Namnet på din Controller
{
    public class PrivatController : BaseController // PrivatController ärver från BaseController
    {
        private readonly CVContext _dbContext; // Databaskontexten för privatprofilen

        public PrivatController(CVContext dbContext) : base(dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldela den inkommande databaskontexten till det privata fältet
        }

        [HttpGet]
        public IActionResult PrivatProfil() // Visar användarens privatprofilsinställningar
        {
            if (User.Identity.IsAuthenticated && _dbContext != null) // Kontrollerar autentisering och databaskontextens existens
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta inloggad användares ID
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämta användarobjektet från databasen baserat på användar-ID

                if (user != null) // Om användaren finns
                {
                    var viewModel = new SetPrivatViewModel // Skapa en ViewModel för inställning av privat profil
                    {
                        Privat = user.Privat // Lagra användarens nuvarande privatprofilinställning i ViewModel
                    };
                    return View(viewModel); // Skicka ViewModel till vyn för att visa privatprofilsinställningar
                }
                return RedirectToAction("ChangeInformation"); // Om användaren inte finns, omdirigera till ändra informationssidan
            }
            return RedirectToAction("Login"); // Om inte autentiserad, omdirigera till inloggningssidan eller hantera autentiseringsfel
        }

        [HttpPost]
        public IActionResult PrivatProfil(SetPrivatViewModel model) // Hanterar ändringar i användarens privatprofilinställningar
        {
            SetMessageCount(); // Uppdatera antalet meddelanden för inloggad användare
            if (ModelState.IsValid && User.Identity.IsAuthenticated && _dbContext != null) // Kontrollera modellens giltighet, autentisering och databaskontextens existens
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta inloggad användares ID
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämta användarobjektet från databasen baserat på användar-ID

                if (user != null) // Om användaren finns
                {
                    user.Privat = model.Privat; // Uppdatera användarens privatprofilinställning med värdet från modellen
                    _dbContext.SaveChanges(); // Spara ändringar i databasen
                    return RedirectToAction("ChangeInformation", "Profile"); // Omdirigera till ändra informationssidan i profilen
                }
                return RedirectToAction("ChangeInformation"); // Om användaren inte finns, omdirigera till ändra informationssidan
            }
            return View(model); // Om modellen inte är giltig eller autentiseringen misslyckas, återgå till vyn med felmeddelanden
        }
    }
}
