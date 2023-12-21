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
            List<Person> listAvPersoner = _dbContext.Personer.ToList();
            ViewBag.Meddelande = "Listan med profiler";
            // TODO: Implementera databaslogiken
            // var projekten = // Hämta projektdata från databasen med LINQ

            return View(listAvPersoner); // Temporärt tills databaslogiken är implementerad

        }

        //Hugos sökruta
        [HttpGet]
        public IActionResult Search(string searchTerm)
        {
            // Implementera söklogiken för profiler här baserat på searchTerm
            List<Person> sökResultat = _dbContext.Personer
                .Where(p => p.Förnamn.Contains(searchTerm) || p.Efternamn.Contains(searchTerm)) // Exempel: Sök efter profiler med förnamn eller efternamn som innehåller söktermen
                .ToList();

            if (sökResultat.Count == 0)
            {
                ViewBag.ErrorMessage = "Inga matchningar hittades för din sökning.";
            }

            return View("ProfilePage", sökResultat); // Visa sökresultaten på samma vyn som ProfilePage
        }
        //PS: använder Förnamn och Efternamn här istället för Fullname på kodrad 32
        //slut på sökrutan

        public IActionResult ChangeInformation()
        {
            return View();
        }






    }



}
