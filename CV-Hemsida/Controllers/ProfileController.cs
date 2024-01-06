using CVDataLayer; // Importera CVDataLayer för att få tillgång till databasen
using CVModels; // Importera CVModels för att använda modellklasser
using CVModels.ViewModels; // Importera ViewModel för att hantera vyn för profilsidan
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasoperationer
using System.Security.Claims; // Importera Claims för att hantera användarinformation

namespace CV_Hemsida.Controllers // Namnet på din Controller
{
    public class ProfileController : BaseController // ProfileController ärver från BaseController för återanvändning av funktionalitet
    {
        private CVContext _dbContext; // Databaskontexten för profilhantering

        public ProfileController(CVContext dbContext) : base(dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldela den inkommande databaskontexten till det privata fältet
        }

        // Visar en lista med profiler som inte är privata
        public IActionResult ProfilePage()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            var nonPrivateProfiles = _dbContext.Personer
                .Include(p => p.User)
                .Where(p => !p.User.Privat)
                .ToList();

            ViewBag.Meddelande = "Listan med profiler";

            return View(nonPrivateProfiles); // Visa vyn för profiler
        }

        // Visar en användares profil baserat på användar-ID
        [HttpGet]
        public IActionResult ViewProfile(string userId)
        {
            // Hämta användarprofilen från databasen baserat på användar-ID
            var profileUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (profileUser == null)
            {
                return NotFound(); // Returnera 404 om profilen inte finns
            }

            // Om profilen är privat och användaren inte är inloggad, omdirigera till inloggningssidan
            if (profileUser.Privat && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account");
            }

            // Om profilen är privat och användaren är inloggad eller om profilen inte är privat, visa profilen
            var profile = profileUser.Person;

            if (profile == null)
            {
                return NotFound(); // Returnera 404 om profilen inte finns
            }

            return View(profile); // Visa vyn för den specifika profilen
        }

        // Söker efter profiler baserat på en sökterm
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            // Konvertera söktermen till gemener för att utföra en icke-skiftlägeskänslig sökning
            searchTerm = searchTerm?.ToLower();

            if (string.IsNullOrWhiteSpace(searchTerm))
            {
                return View("ProfilePage"); // Visa profilsidan om söktermen är tom
            }

            // Hämta alla personer från databasen
            var allPersons = _dbContext.Personer
                .Include(p => p.User)
                .ToList();

            // Implementera söklogik för profiler baserat på söktermen i minnet
            List<Person> searchResults = allPersons
                .Where(p => !p.User.Privat &&
                    (p.FullName().ToLower().Contains(searchTerm) ||
                     p.Förnamn.ToLower().Contains(searchTerm) ||
                     p.Efternamn.ToLower().Contains(searchTerm)))
                .ToList();

            if (searchResults.Count == 0)
            {
                ViewBag.ErrorMessage = "Inga matchningar hittades för din sökning."; // Visa felmeddelande om ingen matchning hittades
            }

            return View("ProfilePage", searchResults); // Visa sökresultaten på samma vy som profilsidan
        }

        // Visar användarens information för att göra ändringar
        [HttpGet]
        public IActionResult ChangeInformation()
        {
            SetMessageCount(); // Uppdatera antalet meddelanden innan vyn returneras
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta ID för inloggad användare
            var userPerson = _dbContext.Personer.FirstOrDefault(p => p.AnvändarID == userId); // Hämta användarobjektet från databasen baserat på användar-ID

            if (userPerson == null)
            {
                return RedirectToAction("Index", "Home"); // Omdirigera om användarens information inte finns
            }

            // Kartlägg användarens information till ChangeInformationViewModel
            var viewModel = new ChangeInformationViewModel
            {
                Förnamn = userPerson.Förnamn,
                Efternamn = userPerson.Efternamn,
                Adress = userPerson.Adress
            };

            return View(viewModel); // Visa vyn för att ändra information
        }

        // Sparar användarens ändrade information till databasen
        [HttpPost]
        public IActionResult SaveInfo(ChangeInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta ID för inloggad användare
                var userPerson = _dbContext.Personer.FirstOrDefault(p => p.AnvändarID == userId); // Hämta användarobjektet från databasen baserat på användar-ID

                if (userPerson == null)
                {
                    return RedirectToAction("ChangeInformation", model); // Omdirigera om användarens information inte finns
                }

                // Uppdatera användarens information med värdena från formuläret
                userPerson.Förnamn = model.Förnamn;
                userPerson.Efternamn = model.Efternamn;
                userPerson.Adress = model.Adress;

                _dbContext.SaveChanges(); // Spara ändringar direkt till databasen

                return RedirectToAction("ChangeInformation", model); // Omdirigera till användarens profilsida
            }

            // Returnera till vyn för att ändra information med valideringsfel om ModelState inte är giltig
            return View("ChangeInformation", model);
        }

        // Visar en användares profil baserat på en identifierare (t.ex. användarens e-post eller användarnamn)
        public IActionResult VisaAnvändaresProfil(string id)
        {
            // Antag att id är användarens unika identifierare (t.ex. e-post eller användarnamn)
            var user = _dbContext.Users
                        .Include(u => u.Cv)
                        .FirstOrDefault(u => u.Email == id);

            if (user == null || user.Cv == null)
            {
                return RedirectToAction("ResourceNotFound"); // Visa en lämplig sida om användaren eller användarens CV inte hittas
            }
            else
            {
                var cvViewModel = new AnvändareCVViewModel
                {
                    // Fyll i all information från user och user.Cv här
                    Namn = user.UserName,
                    Kompetenser = user.Cv.Kompetenser,
                    Utbildningar = user.Cv.Utbildningar,
                    TidigareErfarenhet = user.Cv.TidigareErfarenhet,
                    ProfilbildPath = user.Cv.ProfilbildPath,
                    // Lägg till andra relevanta egenskaper här
                };

                // Skicka denna information till vyn för att visa användarens profilsida
                return View("VisaAnvändaresProfil", cvViewModel); // Se till att du har en vy som heter "VisaAnvändaresProfil"
            }
        }

        // Visar en privat profil
        [HttpGet]
        public IActionResult PrivatProfil()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier); // Hämta ID för inloggad användare
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId); // Hämta användarobjektet från databasen baserat på användar-ID

            if (user == null)
            {
                return RedirectToAction("ChangeInformation"); // Omdirigera om användarens information inte finns
            }

            var viewModel = new SetPrivatViewModel
            {
                Privat = user.Privat // Hämta användarens privatprofilinställning
            };

            return View(viewModel); // Visa vyn för privat profil
        }

        // Visar en personlig profilsida baserad på användar-ID
        [HttpGet]
        public IActionResult PersonalProfilePage(string userId)
        {
            var user = _dbContext.Users
                            .Include(u => u.Person)
                            .FirstOrDefault(u => u.Id == userId);
            var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var currentUser = _dbContext.Users.FirstOrDefault(u => u.Id == currentUserId);

            if (user == null)
            {
                return NotFound(); // Returnera 404 om användaren inte hittas
            }

            var viewModel = new ProfileViewModel
            {
                FullName = user.Person!.FullName(), // Hämta användarens fullständiga namn
                UserName = user.UserName!, // Hämta användarens användarnamn
                Address = user.Person.Adress, // Hämta användarens adress
                IsSelf = user.Id == currentUserId, // Kontrollera om användaren är inloggad och visar sin egen profil
                CurrentUserFullName = User.Identity.IsAuthenticated ? currentUser.Person.FullName() : string.Empty // Hämta inloggad användares fullständiga namn om användaren är inloggad
            };

            return View("PersonalProfilePage", viewModel); // Visa den personliga profilsidan
        }


        // Skickar ett meddelande till en användare
        [HttpPost]
        public IActionResult SendMessage(ProfileViewModel model)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == model.UserName);

            if (user == null)
            {
                return RedirectToAction("Index"); // Omdirigera om användaren inte hittas
            }

            if (model.CurrentUserFullName == null)
                {
                    return RedirectToAction("MeddelandeFailed", "Meddelande");
                }

            if (model.Message == null)
            {
                return RedirectToAction("MeddelandeFailed", "Meddelande");
            }

            _dbContext.Meddelande.Add(new Meddelande
            {
                Användare = user, // Användare som meddelandet skickas till
                Avsändare = model.CurrentUserFullName, // Avsändarens namn
                Innehåll = model.Message, // Meddelandets innehåll
                Läst = false, // Sätt läststatus till falsk
            });

            _dbContext.SaveChanges(); // Spara meddelandet till databasen

            return RedirectToAction("MeddelandeConfirm", "Meddelande"); // Omdirigera till bekräftelsesidan efter att meddelandet skickats
        }


    }
}