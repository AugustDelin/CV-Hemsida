//Denna kontrollerklass(HomeController) inneh�ller metoder f�r att
//hantera beg�randen f�r att visa startsidan, sekretessinformationen och hantering av fel.
//Den anv�nder ILogger f�r att hantera loggning och
//returnerar motsvarande vyer baserat p� beg�randena.

using CVWebbAPI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace CVWebbAPI.Controllers
{
	public class HomeController : Controller
	{
		private readonly ILogger<HomeController> _logger;

		public HomeController(ILogger<HomeController> logger)
		{
			_logger = logger;
		}

		public IActionResult Index()
		{
			return View();
		}

		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
