using Microsoft.AspNetCore.Mvc;
using CVModels;
using CVDataLayer;
using Microsoft.AspNetCore.Identity;
using CVModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using CVModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System;
using CV_SITE.Repositories;

namespace CV_Hemsida.Controllers
{
    public class AccountController : BaseController
    {
        private readonly UserManager<Användare>? _userManager;
        private readonly SignInManager<Användare>? _signInManager;
        private readonly CVContext _dbContext;

        public AccountController(UserManager<Användare> userManager, SignInManager<Användare> signInManager, CVContext dbContext) : base(dbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dbContext = dbContext;
            
        }

        

        public IActionResult Login()
        {
            SetMessageCount();
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            SetMessageCount();
            if (ModelState.IsValid)
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


        
        public async Task<IActionResult> Logout()
        {
            SetMessageCount();
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login");
        }




        public IActionResult Inloggad()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            SetMessageCount();
            return View();
        }


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

        public IActionResult RegisterPerson()
        {
            SetMessageCount();
            return View();
        }


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
                _dbContext.SaveChanges();

                return RedirectToAction("Index", "Home"); // Redirect to a success page
            }

            return View(model);
        }




        [HttpPost]
        public async Task<IActionResult> EditPassword(EditPasswordViewModel model)
        {
            
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

        public IActionResult PasswordConfirmation()
        {
            return View();
        }

        public IActionResult EditPassword()
        {
            SetMessageCount();
            return View();
        }
    }
}