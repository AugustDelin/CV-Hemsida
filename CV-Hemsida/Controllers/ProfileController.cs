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
            var nonPrivateProfiles = _dbContext.Personer
                .Include(p => p.User) // Assuming Användare is the related user entity
                .Where(p => !p.User.Privat)
                .ToList();

            ViewBag.Meddelande = "Listan med profiler";

            return View(nonPrivateProfiles);
        }


        [HttpGet]
        public IActionResult ViewProfile(string userId)
        {
            var profileUser = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (profileUser == null)
            {
                return NotFound(); // Om profilen inte finns, returnera 404
            }

            if (profileUser.Privat && !User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login", "Account"); // Användaren är inte inloggad, omdirigera till inloggningssidan
            }

            // Om profilen är privat men användaren är inloggad eller om profilen inte är privat, visa profilen
            var profile = profileUser.Person;

            if (profile == null)
            {
                return NotFound(); // Om profilen inte finns, returnera 404
            }

            return View(profile);
        }



        //Hugos sökruta
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            searchTerm = searchTerm?.ToLower(); // Convert the search term to lowercase (or use .ToUpper() for case-insensitive search)

            // Fetch all the data from the database first
            var allPersons = _dbContext.Personer
                .Include(p => p.User) // Ensure User is included in the query
                .ToList();

            // Implement search logic for profiles based on searchTerm in-memory
            List<Person> searchResults = allPersons
                .Where(p => !p.User.Privat &&
                    (p.FullName().ToLower().Contains(searchTerm) ||
                     p.Förnamn.ToLower().Contains(searchTerm) ||
                     p.Efternamn.ToLower().Contains(searchTerm)))
                .ToList();

            if (searchResults.Count == 0)
            {
                ViewBag.ErrorMessage = "Inga matchningar hittades för din sökning.";
            }

            return View("ProfilePage", searchResults); // Display search results on the same view as ProfilePage
        }





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





        public IActionResult VisaAnvändaresProfil(string id)
        {
            // Här antar vi att id är användarens unika identifierare (som användarens e-post eller användarnamn)
            var user = _dbContext.Users
                        .Include(u => u.Cv)
                        .FirstOrDefault(u => u.Email == id);

            if (user == null || user.Cv == null)
            {
                // Om användaren inte hittas, visa en lämplig sida
                return RedirectToAction("ResourceNotFound");
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

                // Skicka denna information till vyn som ska visa användarens profilsida
                return View("VisaAnvändaresProfil", cvViewModel); // Se till att du har en vy som heter "VisaAnvändaresProfil"
            }
        }



        [HttpGet]
        public IActionResult PrivatProfil()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

            if (user == null)
            {
                return RedirectToAction("ChangeInformation");
            }

            var viewModel = new SetPrivatViewModel
            {
                Privat = user.Privat
            };

            return View(viewModel);
        }

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
                return NotFound();
            }

            var viewModel = new ProfileViewModel
            {
                FullName = user.Person!.FullName(),
                UserName = user.UserName!,
                Address = user.Person.Adress,
                IsSelf = user.Id == currentUserId,
                CurrentUserFullName = User.Identity.IsAuthenticated ? currentUser.Person.FullName() : string.Empty
            };

            return View("PersonalProfilePage", viewModel);
        }

        [HttpPost]
        public IActionResult SendMessage(ProfileViewModel model)
        {
            var user = _dbContext.Users.FirstOrDefault(x => x.UserName == model.UserName);
            _dbContext.Meddelande.Add(new Meddelande
            {
                Användare = user,
                Avsändare = model.CurrentUserFullName,
                Innehåll = model.Message,
                Läst = false,
            });

            _dbContext.SaveChanges();

            return RedirectToAction("Index");

        }
    }
}