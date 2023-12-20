using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class ProjektController : Controller
    {
        private CVContext _dbContext;

        public ProjektController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }
        public IActionResult ProjectPage()
        {
            List<Projekt> listAvProjekt = _dbContext.Projekts.ToList();
            ViewBag.Meddelande = "Listan med produkter";
            // TODO: Implementera databaslogiken
            // var projekten = // Hämta projektdata från databasen med LINQ

            return View(listAvProjekt); // Temporärt tills databaslogiken är implementerad

        }



  
    }



}
