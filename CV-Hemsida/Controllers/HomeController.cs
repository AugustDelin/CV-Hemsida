using CVDataLayer;
using CVModels;
using Microsoft.AspNetCore.Mvc;

namespace CV_Hemsida.Controllers
{
    public class HomeController : Controller
    {
        private CVContext _dbContext;
        public IActionResult Index()
        {
            List<CV> listAvCV = _dbContext.CVs.ToList();
            return View(listAvCV);
        }
    }
}
