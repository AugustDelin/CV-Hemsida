using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CV_Hemsida.Controllers
{
    public class ProfileController : Controller
    {
        private CVContext _dbContext;

        public ProfileController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult ProfilePage()
        {
            List<Person> listAvPersoner = _dbContext.Personer.ToList();
            ViewBag.Meddelande = "Listan med profiler";
            // TODO: Implementera databaslogiken
            // var projekten = // Hämta projektdata från databasen med LINQ

            return View(listAvPersoner); // Temporärt tills databaslogiken är implementerad

        }

        //Hugos sökruta
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            // Implementera söklogiken för profiler här baserat på searchTerm
            List<Person> sökResultat = _dbContext.Personer
                .Where(p => p.Förnamn.Contains(searchTerm) || p.Efternamn.Contains(searchTerm)) // Exempel: Sök efter profiler med förnamn eller efternamn som innehåller söktermen
                .ToList();

            if (sökResultat.Count == 0)
            {
                ViewBag.ErrorMessage = "Inga matchningar hittades för din sökning.";
            }

            return View("ProfilePage", sökResultat); // Visa sökresultaten på samma vyn som ProfilePage
        }
        //PS: använder Förnamn och Efternamn här istället för Fullname på kodrad 32
        //slut på sökrutan

        [HttpGet]
        public IActionResult ChangeInformation()
        {
            // Retrieve the current user's ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Use the user ID to retrieve the corresponding Person from the database
            Person userPerson = _dbContext.Personer.FirstOrDefault(p => p.AnvändarID == userId);

            if (userPerson == null)
            {
                // Handle the case where the user's information is not found
                return RedirectToAction("Index", "Home");
            }

            // Map the user's information to the ChangeInformationViewModel
            var viewModel = new ChangeInformationViewModel
            {
                Förnamn = userPerson.Förnamn,
                Efternamn = userPerson.Efternamn,
                Adress = userPerson.Adress
            };

            return View(viewModel);
        }


        [HttpPost]
        public IActionResult SaveInfo(ChangeInformationViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the current user's ID
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Use the user ID to retrieve the corresponding Person from the database
                Person userPerson = _dbContext.Personer.FirstOrDefault(p => p.AnvändarID == userId);

                if (userPerson == null)
                {
                    // Handle the case where the user's information is not found
                    return RedirectToAction("ChangeInformation", model);
                }

                // Update the user's information with the values from the form
                userPerson.Förnamn = model.Förnamn;
                userPerson.Efternamn = model.Efternamn;
                userPerson.Adress = model.Adress;

                // Save changes directly to the database
                _dbContext.SaveChanges();

                return RedirectToAction("ChangeInformation", model); // Redirect to the user's profile page
            }

            // If ModelState is not valid, return to the ChangeInformation view with validation errors
            return View("ChangeInformation", model);
        }


        public async Task<IActionResult> VisaAnvändaresProfil(string identifier)
        {
            // Försök först hitta användaren baserat på ID
            var användare = await _dbContext.Users
                .Include(u => u.Cv)
                .FirstOrDefaultAsync(u => u.Id == identifier);

            // Om ingen användare hittas, försök med e-post
            if (användare == null)
            {
                användare = await _dbContext.Users
                    .Include(u => u.Cv)
                    .FirstOrDefaultAsync(u => u.Email == identifier);
            }

            if (användare == null || användare.Cv == null)
            {
                return RedirectToAction("ResourceNotFound");
            }

          


            var cvViewModel = new AnvändareCVViewModel
            {
                Namn = användare.UserName,  // Eller något annat relevant fält för namn
                Kompetenser = användare.Cv.Kompetenser,
                Utbildningar = användare.Cv.Utbildningar,
                TidigareErfarenhet = användare.Cv.TidigareErfarenhet,
                ProfilbildPath = användare.Cv.ProfilbildPath,
                // Om det finns andra fält i Cv som ska visas, lägg till dem här
            };

            if (användare.Cv.DeltarIProjekt != null)
            {
                cvViewModel.DeltarIProjekt = användare.Cv.DeltarIProjekt.Select(dp => new ProjektViewModel
                {
                    Titel = dp.Proj.Titel,
                    Beskrivning = dp.Proj.Beskrivning
                    // Lägg till fler fält om nödvändigt
                }).ToList();
            }



            return View("EnskildProfilePage", cvViewModel);
        }




    }


}
