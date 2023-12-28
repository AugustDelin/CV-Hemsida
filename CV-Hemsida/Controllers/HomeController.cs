using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;
using CV_Hemsida;
using CVModels.ViewModels;
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

        public IActionResult Index()
        {
            var användareViewModels = _dbContext.Users
                .Select(u => new AnvändareViewModel
                {
                    Id = u.Id,
                    Namn = u.UserName, // Använd UserName istället för Email
                                       // Andra egenskaper som du vill inkludera
                })
                .ToList();

            return View(användareViewModels);
        }


    }
}
