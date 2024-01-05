using Microsoft.AspNetCore.Mvc;
using CVModels; // Importerar modellerna för användare och person
using CVDataLayer; // Importerar dataåtkomstlagret
using Microsoft.AspNetCore.Identity; // Importerar Identity-funktioner för hantering av användare och inloggning
using CVModels.ViewModel; // Importerar vymodeller för användare och person
using Microsoft.AspNetCore.Authorization; // Importerar autorisationsattribut
using CVModels.ViewModels; // Importerar vymodeller för användare och person
using Microsoft.EntityFrameworkCore; // Importerar Entity Framework Core för databashantering
using System; // Importerar systembiblioteket för allmänna funktioner
using CV_SITE.Repositories; // Importerar specifika repositories

namespace CV_Hemsida.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<Användare>? _userManager; // Hanterar användaråtgärder
        private readonly SignInManager<Användare>? _signInManager; // Hanterar inloggning/utloggning
        private readonly CVContext _dbContext; // Databaskontext för användar- och personinformation

        // Konstruktor för AccountController
        public AccountController(UserManager<Användare> userManager, SignInManager<Användare> signInManager, CVContext dbContext) : base(dbContext)
        {
            _userManager = userManager; // Initierar UserManager
            _signInManager = signInManager; // Initierar SignInManager
            _dbContext = dbContext; // Initierar CVContext

        }


        // Metod för att visa inloggningsvyn
        public IActionResult Login()
        {
            SetMessageCount(); // Uppdaterar meddelandelicensräknaren
            return View(); // Visar inloggningsvyn
        }


        // Metod för att hantera inloggning
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            SetMessageCount();
            if (ModelState.IsValid) // Uppdaterar meddelandelicensräknaren
            {
                var user = await _userManager.FindByNameAsync(model.UserName);

                if (user != null)
                {
                    var result = await _signInManager.PasswordSignInAsync(user, model.Lösenord, model.Active, lockoutOnFailure: false);

                    if (result.Succeeded)
                    {
                        // Användaren har loggats in framgångsrikt, omdirigera till en annan sida
                        return RedirectToAction("Index", "Home");
                    }
                }

                ModelState.AddModelError(string.Empty, "Felaktigt användarnamn eller lösenord.");
            }

            // Om inloggningen misslyckades eller ModelState inte är giltig, returnera vyn med felmeddelanden
            return View(model);
        }


        // Metod för att logga ut användaren
        public async Task<IActionResult> Logout()
        {
            SetMessageCount(); // Uppdaterar meddelandelicensräknaren
            await _signInManager.SignOutAsync(); // Loggar ut användaren
            return RedirectToAction("Login"); // Omdirigerar till inloggningssidan
        }



        // Metod för att visa inloggad-vyn
        public IActionResult Inloggad()
        {
            return View(); // Visar inloggad-vyn
        }


        // Metod för att visa registreringsvyn för användare
        public IActionResult RegisterUser()
        {
            SetMessageCount(); // Uppdaterar meddelandelicensräknaren
            return View(); // Visar registreringsvyn för användare
        }


        // Metod för att hantera registrering av användare
        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new Användare
                {
                    UserName = model.Email,
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    TempData["UserId"] = user.Id;

                    return RedirectToAction("RegisterPerson", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }

        // Metod för att visa registreringsvyn för person
        public IActionResult RegisterPerson()
        {
            SetMessageCount(); // Uppdaterar meddelandelicensräknaren
            return View(); // Visar registreringsvyn för person
        }


        // Metod för att hantera registrering av person
        [HttpPost]
        public async Task<IActionResult> RegisterPerson(RegisterPersonViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Retrieve the user ID from TempData
                string userId = TempData["UserId"]?.ToString();

                if (string.IsNullOrEmpty(userId))
                {
                    // Handle the case where UserId is missing or null
                    return RedirectToAction("RegisterUser");
                }

                var person = new Person
                {
                    Personnummer = model.Personnummer,
                    Förnamn = model.Förnamn,
                    Efternamn = model.Efternamn,
                    Adress = model.Adress,
                    AnvändarID = userId
                };

                // Add the new person to the DbContext
                _dbContext.Personer.Add(person);

                // Save changes directly to the database
                await _dbContext.SaveChangesAsync();

                return RedirectToAction("Index", "Home"); // Redirect to a success page
            }

            return View(model);
        }



        // Metod för att ändra lösenord
        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
        {
            SetMessageCount();
            try
            {
                if (ModelState.IsValid)
                {
                    var user = await _userManager.GetUserAsync(User);
                    if (user == null)
                    {
                        return RedirectToAction("Login", "Account");
                    }

                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, model.NuvarandeLösenord, model.NyttLösenord);

                    if (changePasswordResult.Succeeded)
                    {
                        // Password change successful!
                        return View("PasswordConfirmation", "Account");
                    }
                    else
                    {
                        foreach (var error in changePasswordResult.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                        return View(model);
                    }
                }

                return View(model);
            }
            catch (Exception ex)
            {
                // Log any unexpected exception
                // You may want to log the exception details to a logging system
                return View(model);
            }
        }

        // Metod för att visa bekräftelsesidan för lösenordsändring
        public IActionResult PasswordConfirmation()
        {
            return View(); // Visar bekräftelsesidan för lösenordsändring
        }

        // Metod för att visa vyn för lösenordsändring
        public IActionResult EditPassword()
        {
            return View(); // Visar vyn för lösenordsändring
        }
    }
}