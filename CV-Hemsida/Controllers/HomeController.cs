using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;



namespace CV_Hemsida.Controllers
{
    public class HomeController : Controller
    {
        private CVContext _dbContext;

        public HomeController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        // I HomeController.cs
        public IActionResult Index()
        {
            var användareViewModels = _dbContext.Users
                .Include(u => u.Cvs)
                .Select(u => new AnvändareViewModel
                {
                    Id = u.Id.ToString(), // Antag att Id är en string. Om det är en int, konvertera den som behövs.
                    Namn = u.UserName, // Eller den egenskap du har för namn
                    ProfilbildUrl = u.Cvs != null && u.Cvs.Any() ? u.Cvs.FirstOrDefault().ProfilbildPath : null,
                    Kompetenser = u.Cvs != null && u.Cvs.Any() ? string.Join(", ", u.Cvs.Select(cv => cv.Kompetenser)) : null
                }).ToList();

            return View(användareViewModels);
        }





    }
}
