using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace CV_Hemsida.Controllers
{
    public class PrivatController : Controller
    {

        private readonly CVContext _dbContext;

        public PrivatController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

       

        [HttpGet]
        public IActionResult PrivatProfil1()
        {
            if (User.Identity.IsAuthenticated && _dbContext != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    var viewModel = new SetPrivatViewModel
                    {
                        Privat = user.Privat
                    };
                    return View(viewModel);
                }
                return RedirectToAction("ChangeInformation");
            }
            return RedirectToAction("Login"); // Eller hantera autentiseringsfel
        }

        [HttpPost]
        public IActionResult PrivatProfil(SetPrivatViewModel model)
        {
            if (ModelState.IsValid && User.Identity.IsAuthenticated && _dbContext != null)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                if (user != null)
                {
                    user.Privat = model.Privat;
                    _dbContext.SaveChanges();
                    return RedirectToAction("ChangeInformation", "Profile");
                }
                return RedirectToAction("ChangeInformation");
            }
            return View(model); // Återgå till vyn med felmeddelanden
        }

        //public IActionResult PrivatProfil2()
        //{
        //    if (User.Identity.IsAuthenticated && _dbContext != null)
        //    {
        //        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        //        var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

        //        if (user != null)
        //        {
        //            var viewModel = new SetPrivatViewModel
        //            {
        //                Privat = user.Privat
        //            };
        //            TempData["IsProfilePrivate"] = viewModel.Privat; // Spara flaggan i TempData
        //            return View(viewModel);
        //        }
        //        return RedirectToAction("ChangeInformation");
        //    }
        //    return RedirectToAction("Login"); // Eller hantera autentiseringsfel
        //}

     





    }
}
