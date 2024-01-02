using CVDataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVModels;
using System.Security.Claims;
using CVModels.ViewModels;

namespace CV_Hemsida.Controllers
{
    public class CVController : Controller
    {
        private CVContext _dbContext;

        public CVController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CVPage()
        {
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


        public IActionResult RegisterCV()
        {
            return View();
        }

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


        [HttpGet]
        public IActionResult DeleteCV(int id)
        {
            var cvToRemove = _dbContext.CVs.Find(id);

            if (cvToRemove == null)
            {
                return NotFound(); // If CV is not found, return NotFound
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

            _dbContext.CVs.Remove(cvToRemove);
            _dbContext.SaveChanges();

            return RedirectToAction("CVPage");
        }

        public IActionResult VisaAnvändaresCV(string användarId)
        {
            var användare = _dbContext.Users
                              .Include(u => u.Cv)
                              .FirstOrDefault(u => u.Id == användarId);

            if (användare?.Cv != null)
            {
                var cv = användare.Cv;
                var cvViewModel = new AnvändareCVViewModel
                {
                    // Fyll i alla nödvändiga fält från cv och användare
                };
                return View(cvViewModel);
            }
            else
            {
                return RedirectToAction("ResourceNotFound");
            }
        }

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



        [HttpGet]
        public IActionResult ChangeCV(int id)
        {
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
                return RedirectToAction("AccessDenied", "Authorization"); // Skapa en passande åtkomstnekat-vy
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

            return RedirectToAction("ResourceNotFound"); // Handle the case where either the CV or project is not found
        }





        public IActionResult ChangeCV()
        {
            return View();
        }



        public ActionResult ResourceNotFound()
        {
            return View();
        }
    }
}
