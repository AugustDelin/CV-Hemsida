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
            // Hämta CVs
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

            // Hämta det senaste projektet
            var latestProject = _dbContext.Projekts
                .OrderByDescending(p => p.Id)
                .Select(p => new ProjektViewModel
                {
                    Id = p.Id,
                    Titel = p.Titel,
                    Beskrivning = p.Beskrivning
                })
                .FirstOrDefault();

            // Skapa en CombinedViewModel instans
            var viewModel = new CombinedViewModel
            {
                CVs = cvViewModels,
                StartPage = new StartPageViewModel
                {
                    LatestProject = latestProject
                }
            };

            // Skicka CombinedViewModel till vyn
            return View(viewModel);
        }

    }

}
