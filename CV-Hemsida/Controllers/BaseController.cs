using CVDataLayer; // Importera det dataåtkomstlager som innehåller kontexten för att få åtkomst till databasen
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionaliteten
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasåtkomst
using System.Security.Claims; // Importera Claims för användarinformation

public class BaseController : Controller // En bascontroller för andra controllrar att ärva från
{
    private readonly CVContext _dbContext; // En instans av databaskontexten för att interagera med databasen

    public BaseController(CVContext dbContext) // En konstruktor som tar emot databaskontexten som parameter
    {
        _dbContext = dbContext; // Tilldelar den inkommande databaskontexten till det privata fältet
    }

    public void SetMessageCount() // En metod för att räkna antalet olästa meddelanden för en inloggad användare
    {
        if (User.Identity.IsAuthenticated) // Kontrollerar om användaren är inloggad
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämtar användar-ID från inloggad användares Claims
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämtar användarinformation från databasen baserat på användar-ID

            var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id && !x.Läst).ToList();
            // Hämtar olästa meddelanden för den inloggade användaren från databasen

            ViewBag.Meddelanden = messages.Count; // Sätter antalet olästa meddelanden i ViewBag för användning i vyer
        }
    }
}