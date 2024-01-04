using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;
using CVModels.ViewModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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
            if (User.Identity.IsAuthenticated)
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var user = _dbContext.Users.FirstOrDefault(u => u.Id == userId);

                var messages = _dbContext.Meddelande.Where(x => x.Mottagare == user.Id && !x.Läst).ToList();

                ViewBag.Meddelanden = messages.Count;
            }

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