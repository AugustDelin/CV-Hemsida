using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class ProfileController : Controller
    {
        private CVContext _dbContext;

        public ProfileController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult ProfilePage()
        {
            List<Användare> listAvProfiler = _dbContext.Users.ToList();
            ViewBag.Meddelande = "Listan med profiler";
            // TODO: Implementera databaslogiken
            // var projekten = // Hämta projektdata från databasen med LINQ

            return View(listAvProfiler); // Temporärt tills databaslogiken är implementerad

        }




    }



}
