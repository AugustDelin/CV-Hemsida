using CVDataLayer; // Importera dataåtkomstlagret
using Microsoft.AspNetCore.Mvc; // Importera ASP.NET Core MVC-funktionalitet
using Microsoft.EntityFrameworkCore; // Importera Entity Framework Core för databasåtkomst
using CVModels; // Importera CV-relaterade modeller
using System.Security.Claims; // Importera Claims för användarinformation
using CVModels.ViewModels; // Importera CV-relaterade view-modeller

namespace CV_Hemsida.Controllers
{
    public class CVController : BaseController // CVController ärver från BaseController
    {
        private CVContext _dbContext; // Databaskontexten för CV

        public CVController(CVContext dbContext) : base(dbContext) // Konstruktor som tar emot databaskontexten
        {
            _dbContext = dbContext; // Tilldelar den inkommande databaskontexten till det privata fältet
        }

        public IActionResult CVPage() // Hanterar vyn för CV-sidan
        {
            SetMessageCount();
            // Hämta den inloggade användarens ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Hämta alla CVs för den inloggade användaren
            var userCVs = _dbContext.CVs
                .Include(cv => cv.User)
                .Where(cv => cv.User.Id == userId)
                .ToList();
            
            var allProjects = _dbContext.Projekts.ToList();

            // Set the ViewBag for the dropdown in the view
            ViewBag.AllProjects = allProjects;

            if (userCVs != null && userCVs.Any())
            {
                // Om det finns CVs, skicka dem till vyn
                return View(userCVs);
            }
            else
            {
                // Om det inte finns CVs, visa ett meddelande i vyn
                ViewBag.Meddelande = "Inga CVs hittades för användaren.";
                return View();
            }
        }


        // Visa vyn för att registrera ett nytt CV
        public IActionResult RegisterCV()
        {
            SetMessageCount();
            return View();
        }


        // Logik för att registrera ett nytt CV baserat på formulärdata och uppladdning av profilbild
        // Lägger till CV i databasen och om allt går bra, omdirigerar till CV-sidan
        // Annars visas vyn för att registrera ett nytt CV med felmeddelanden
        [HttpPost]
        public IActionResult RegisterCV(RegisterCVViewModel model, IFormFile ProfilbildPath)
        {
            // Remove the ModelState error for ProfilbildPath
            ModelState.Remove("ProfilbildPath");

            if (ModelState.IsValid)
            {
                // Get the user ID of the currently logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Create a new CV instance
                var newCV = new CV
                {
                    Kompetenser = model.Kompetenser,
                    Utbildningar = model.Utbildningar,
                    TidigareErfarenhet = model.TidigareErfarenhet,
                    AnvändarId = userId
                };

                // Handle file upload if a profile picture is selected
                if (ProfilbildPath != null && ProfilbildPath.Length > 0)
                {
                    // Specify the directory path
                    var directoryPath = Path.Combine("wwwroot/Bilder");

                    // Create the directory if it doesn't exist
                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    // Generate a unique file name or use the user's ID as the file name
                    var fileName = userId + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilbildPath.FileName);

                    // Specify the path where the file will be saved
                    var filePath = Path.Combine(directoryPath, fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilbildPath.CopyTo(fileStream);
                    }

                    // Set the ProfilbildPath property to the file path
                    newCV.ProfilbildPath = fileName;
                }

                // Add the new CV to the database
                _dbContext.CVs.Add(newCV);
                _dbContext.SaveChanges();

                // Redirect to the CV page
                return RedirectToAction("CVPage");
            }

            // If the model is not valid,
            // return to the RegisterCV view with errors
            return View(model);
        }



        // Metod för att ta bort ett CV baserat på angivet ID
        // Tar bort tillhörande persondeltarprojekt och associerad profilbild
        // Omdirigerar till CV-sidan efter borttagning
        [HttpGet]
        public IActionResult DeleteCV(int id)
        {
            var cvToRemove = _dbContext.CVs.Find(id);

            if (cvToRemove == null)
            {
                return NotFound(); // If CV is not found, return NotFound
            }

            // Explicitly load related PersonDeltarProjekt entries
            _dbContext.Entry(cvToRemove)
                .Collection(c => c.DeltarIProjekt)
                .Load();

            // Remove the associated PersonDeltarProjekt entries
            foreach (var entry in cvToRemove.DeltarIProjekt.ToList())
            {
                _dbContext.PersonDeltarProjekt.Remove(entry);
            }

            // Remove the associated profile picture file
            if (!string.IsNullOrEmpty(cvToRemove.ProfilbildPath))
            {
                var filePath = Path.Combine("wwwroot/Bilder", cvToRemove.ProfilbildPath);

                if (System.IO.File.Exists(filePath))
                {
                    System.IO.File.Delete(filePath);
                }
            }

            // Remove the CV
            _dbContext.CVs.Remove(cvToRemove);
            _dbContext.SaveChanges();


            return RedirectToAction("CVPage");
        }


        // Metod för att visa en specifik användares CV
        // Omdirigerar antingen till användarens personliga profil eller en sida för resursen som inte hittades
        public IActionResult VisaAnvändaresCV(string användarId)
        {
            SetMessageCount();
            var användare = _dbContext.Users
                  .FirstOrDefault(u => u.Id == användarId);

            if (användare != null)
            {
                // Redirect directly to the "PersonalProfilePage" view
                return RedirectToAction("PersonalProfilePage");
            }
            else
            {
                return RedirectToAction("ResourceNotFound");
            }
        }


        // Visar vyn för den personliga profilsidan
        public IActionResult PersonalProfilePage()
        {
            SetMessageCount();
            return View();
        }


        // Logik för att spara ändringar i ett CV
        // Uppdaterar CV-information och profilbild i databasen
        // Omdirigerar till CV-sidan efter sparade ändringar eller visar vyn för att ändra CV med valideringsfel
        [HttpPost]
        public IActionResult SaveChanges(int id, ChangeCVViewModel model, IFormFile ProfilbildPath)
        {
            // Remove the ModelState error for ProfilbildPath
            ModelState.Remove("ProfilbildPath");

            if (ModelState.IsValid)
            {
                var cvToChange = _dbContext.CVs.Find(id);

                if (cvToChange == null)
                {
                    // Handle the case where the CV is not found
                    return RedirectToAction("ChangeCV", new { id = model.Id });
                }

                // Remove the existing profile picture file
                if (!string.IsNullOrEmpty(cvToChange.ProfilbildPath))
                {
                    var existingFilePath = Path.Combine("wwwroot/Bilder", cvToChange.ProfilbildPath);

                    if (System.IO.File.Exists(existingFilePath))
                    {
                        System.IO.File.Delete(existingFilePath);
                    }
                }

                // Handle file upload if a new profile picture is selected
                if (ProfilbildPath != null && ProfilbildPath.Length > 0)
                {
                    // Generate a unique file name or use the user's ID as the file name
                    var fileName = cvToChange.AnvändarId + "_" + Guid.NewGuid().ToString() + "_" + Path.GetFileName(ProfilbildPath.FileName);

                    // Specify the path where the file will be saved
                    var filePath = Path.Combine("wwwroot/Bilder", fileName);

                    // Save the file to the server
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        ProfilbildPath.CopyTo(fileStream);
                    }

                    // Set the ProfilbildPath property to the new file path
                    cvToChange.ProfilbildPath = fileName;
                }

                // Update other CV properties with the new values
                cvToChange.Kompetenser = model.Kompetenser;
                cvToChange.Utbildningar = model.Utbildningar;
                cvToChange.TidigareErfarenhet = model.TidigareErfarenhet;

                // Update the CV in the database
                _dbContext.CVs.Update(cvToChange);
                _dbContext.SaveChanges();

                return RedirectToAction("CVPage"); // Redirect to an appropriate page after saving changes
            }

            // If ModelState is not valid, return to the ChangeCV view with validation errors
            return View("ChangeCV", model);
        }


        // Metod för att visa vyn för att ändra ett specifikt CV baserat på ID
        // Kontrollerar om användaren har rättigheter att ändra CV:t, annars omdirigeras till "ResourceNotFound"-vyn
        [HttpGet]
        public IActionResult ChangeCV(int id)
        {
            SetMessageCount();
            var cvToChange = _dbContext.CVs.Find(id);

            if (cvToChange == null)
            {
                return RedirectToAction("CVPage");
            }

            // Kontrollera om den inloggade användaren är den som skapade CV:t
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (cvToChange.AnvändarId != userId)
            {
                // Användaren har inte rättighet att ändra CV:t
                return RedirectToAction("ResourceNotFound", "CV"); // Skapa en passande åtkomstnekat-vy
            }

            var viewModel = new ChangeCVViewModel
            {
                Id = cvToChange.Id,
                Kompetenser = cvToChange.Kompetenser,
                Utbildningar = cvToChange.Utbildningar,
                TidigareErfarenhet = cvToChange.TidigareErfarenhet,
                ProfilbildPath = cvToChange.ProfilbildPath
            };

            return View(viewModel);
        }



        // Logik för att ansluta ett projekt till ett CV
        // Skapar en ny "DeltarProjekt"-post för att koppla CV:t till projektet
        // Omdirigerar till CV-sidan
        [HttpPost]
        public IActionResult ConnectProjectToCV(int cvId, int projectId)
        {
            var cv = _dbContext.CVs.Include(c => c.User).FirstOrDefault(c => c.Id == cvId);
            var project = _dbContext.Projekts.FirstOrDefault(p => p.Id == projectId);

            if (cv != null && project != null)
            {
                // Check if the connection already exists
                if (_dbContext.PersonDeltarProjekt.Any(dp => dp.Anv.Id == cv.User.Id && dp.Proj.Id == projectId))
                {
                    // Connection already exists, handle accordingly
                    // For example, you can redirect with a message or show an error
                    return RedirectToAction("CVPage");
                }

                // Create a new DeltarProjekt entry to associate the CV with the project
                var deltarProjekt = new DeltarProjekt
                {
                    Anv = cv.User,
                    Proj = project
                };

                // Add the DeltarProjekt entry to the database
                cv.DeltarIProjekt.Add(deltarProjekt);  // Assuming DeltarIProjekt is the navigation property in CV entity
                _dbContext.SaveChanges();

                return RedirectToAction("CVPage");
            }

            return RedirectToAction("CVPage"); // Handle the case where either the CV or project is not found
        }




        // Visa vyn för att ändra CV
        public IActionResult ChangeCV()
        {
            return View();
        }


        // Visa vyn när en resurs inte hittas
        public ActionResult ResourceNotFound()
        {
            return View();
        }
    }
}
