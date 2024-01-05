using System.Security.Claims; // Importera Claims för användarinformation
using CVDataLayer; // Importera CVDataLayer för databasåtkomst
using CVModels.ViewModels; // Importera ViewModel för meddelanden
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet

namespace CV_Hemsida.Controllers // Namnet på din Controller
{
    public class MeddelandeController : BaseController // MeddelandeController ärver från BaseController
    {
        private CVContext _dbContext; // Databaskontexten för meddelanden

        public MeddelandeController(CVContext dbContext) : base(dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldela den inkommande databaskontexten till det privata fältet
        }

        public IActionResult VisaMeddelanden() // Hanterar vyn för att visa meddelanden
        {
            SetMessageCount(); // Uppdatera antalet meddelanden för inloggad användare
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta inloggad användares ID
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämta användarobjektet från databasen baserat på användar-ID

            var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id).ToList(); // Hämta meddelanden för användaren från databasen

            var vm = new MeddelandeViewModel // Skapa en ViewModel för meddelanden
            {
                Meddelanden = messages // Lagra meddelandena i ViewModel-objektet
            };

            return View("Meddelande", vm); // Skicka ViewModel till vyn för meddelandevisning
        }

        public IActionResult Read(int id) // Hanterar läsning av ett specifikt meddelande
        {
            var message = _dbContext.Meddelande.FirstOrDefault(x => x.Id == id); // Hämta meddelandet från databasen baserat på meddelande-ID
            if (message is not null) // Om meddelandet finns
            {
                message.Läst = true; // Markera meddelandet som läst
            }

            _dbContext.SaveChanges(); // Spara ändringar i databasen

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta inloggad användares ID
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämta användarobjektet från databasen baserat på användar-ID

            var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id).ToList(); // Hämta meddelanden för användaren från databasen

            var vm = new MeddelandeViewModel // Skapa en ViewModel för meddelanden
            {
                Meddelanden = messages // Lagra meddelandena i ViewModel-objektet
            };

            return View("Meddelande", vm); // Skicka ViewModel till vyn för meddelandevisning
        }

        public IActionResult MeddelandeConfirm() // Hanterar bekräftelse av meddelande
        {
            SetMessageCount(); // Uppdatera antalet meddelanden för inloggad användare
            return View(); // Visa bekräftelsesidan för meddelande
        }

        public IActionResult MeddelandeFailed() // Hanterar misslyckande för meddelande
        {
            SetMessageCount(); // Uppdatera antalet meddelanden för inloggad användare
            return View(); // Visa sidan för misslyckande med meddelande
        }
    }
}
