using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc;
using CVModels;
using CVDataLayer;
using Microsoft.AspNetCore.Identity;
using CVModels.ViewModel;
using Microsoft.AspNetCore.Authorization;

namespace CV_Hemsida.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;

       

        public IActionResult Login()
        {
            return View();
        }

        public IActionResult Inloggad()
        { 
            return View();
        }

      





    }
}





