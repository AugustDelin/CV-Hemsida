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
        public IActionResult RegisterCV(RegisterCVViewModel model)
        {
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
                    ProfilbildPath = model.ProfilbildPath,
                    AnvändarId = userId
                };

                // Add the new CV to the database
                _dbContext.CVs.Add(newCV);
                _dbContext.SaveChanges();

                // Redirect to the CV page
                return RedirectToAction("CVPage");
            }

            // If the model is not valid, return to the RegisterCV view with errors
            return View(model);
        }

        [HttpGet]
        public IActionResult DeleteCV(int id)
        {
            var cvToRemove = _dbContext.CVs.Find(id);

            if (cvToRemove == null)
            {
                return NotFound(); // Om CV inte hittas, returnera NotFound
            }

            _dbContext.CVs.Remove(cvToRemove);
            _dbContext.SaveChanges();

            return RedirectToAction("CVPage");
        }
    }
}
