using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class ProjektController : Controller
    {
        public IActionResult ProjectPage()
        {

            // TODO: Implementera databaslogiken
            // var projekten = // Hämta projektdata från databasen med LINQ

            return View(/*projekten*/); // Temporärt tills databaslogiken är implementerad

        }


  
    }



}
