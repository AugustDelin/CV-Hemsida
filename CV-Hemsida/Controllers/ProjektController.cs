using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class ProjektController : Controller
    {
        private readonly CVContext _dbContext;

        public ProjektController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult ProjectPage()
        {
            var projekten = _dbContext.Projekts
               .Select(p => new ProjektViewModel
                {
                    Id = p.Id,
                    Titel = p.Titel,
                    Beskrivning = p.Beskrivning,
                    // Andra fält...
                })
                .ToList();

            return View(projekten);
        }
    }



}
