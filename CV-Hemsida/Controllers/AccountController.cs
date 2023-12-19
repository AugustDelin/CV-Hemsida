using Microsoft.AspNetCore.Mvc;
using CVModels;
using CVDataLayer;
using Microsoft.AspNetCore.Identity;
using CVModels.ViewModel;
using Microsoft.AspNetCore.Authorization;
using CVModels.ViewModels;

namespace CV_Hemsida.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User>? _userManager;
        private readonly SignInManager<User>? _signInManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;

        }

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Inloggad()
        {
            return View();
        }

        public IActionResult RegisterUser()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegisterUser(RegisterUserViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new User // Ersätt 'User' med din användarmodell (t.ex. ApplicationUser)
                {
                    UserName = model.Email, // Använd e-posten som användarnamn för enkelhetens skull
                    Email = model.Email
                };

                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    // Användaren skapades framgångsrikt, vidarebefordra användaren till lämplig vy eller åtgärd
                    // Exempelvis, omdirigera användaren till en bekräftelsesida eller en annan sida efter registreringen
                    return RedirectToAction("RegisterLyckades");

                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // Om ModelState inte är giltig eller registreringen misslyckades, returnera vyn med felmeddelanden
            return View(model);
        }







    }
}





