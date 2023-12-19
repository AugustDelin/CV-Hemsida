using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class ProjektController : Controller
    {
        public IActionResult ProjectPage()
        {
            return View();
        }

    }
}
