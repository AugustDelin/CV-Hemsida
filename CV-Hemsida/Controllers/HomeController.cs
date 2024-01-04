using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;
using CVModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

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
            var cvViewModels = _dbContext.CVs
                .Where(cv => !cv.User.Privat)
                .Select(cv => new CVViewModel
                {
                    Id = cv.Id,
                    AnvändarId = cv.User.Id,
                    AnvändarNamn = cv.User.UserName,
                    Kompetenser = cv.Kompetenser,
                    Utbildningar = cv.Utbildningar,
                    TidigareErfarenhet = cv.TidigareErfarenhet
                })
                .ToList();

            return View(cvViewModels);
        }
    }
}
