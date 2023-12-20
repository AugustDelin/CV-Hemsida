using CVDataLayer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CVModels;
using System.Security.Claims;

namespace CV_Hemsida.Controllers
{
    public class CVController : Controller
    {
        private CVContext _dbContext;

        public CVController(CVContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult CVPage()
        {
            // Hämta den inloggade användarens ID
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Hämta CV för den inloggade användaren
            var userCV = _dbContext.CVs
                .Include(cv => cv.User)
                .Where(cv => cv.User.Id == userId)
                .FirstOrDefault();

            if (userCV != null)
            {
                // Om det finns ett CV, skicka det till vyn
                return View(userCV);
            }
            else
            {
                // Om det inte finns ett CV, visa ett meddelande i vyn
                ViewBag.Meddelande = "Inget CV hittades för användaren.";
                return View();
            }
        }

    }
}
