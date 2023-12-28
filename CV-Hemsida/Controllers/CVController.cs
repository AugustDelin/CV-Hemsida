﻿using CVDataLayer;
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
    }
}
