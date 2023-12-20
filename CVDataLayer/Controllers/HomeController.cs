using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
