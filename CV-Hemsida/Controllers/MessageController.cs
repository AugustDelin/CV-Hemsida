using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Message()
        {
            return View();
        }
    }
}
