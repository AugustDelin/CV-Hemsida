using CVDataLayer;
using CVModels;
using CVModels.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Security.Claims;

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
                   Beskrivning = p.Beskrivning
               })
               .ToList();

            return View(projekten);
        }

        public IActionResult Details(int id)
        {
            var projekt = _dbContext.Projekts
                                    .Include(p => p.User) // Inkludera projektets skapare
                                    .FirstOrDefault(p => p.Id == id);
            if (projekt == null)
            {
                return NotFound();
            }

            // Hämta en lista av användare som är kopplade till detta projekt på något sätt.
            var deltagandeAnvändare = _dbContext.Users
                                               .Where(u => u.SkapadeProjekt.Any(p => p.Id == id))
                                               .ToList();

            // Konvertera till AnvändareViewModel
            var deltagandeAnvändareViewModels = deltagandeAnvändare.Select(u => new AnvändareViewModel
            {
                Namn = u.UserName ?? "Okänd Användare" // Hantera nullvärden
            }).ToList();

            var projektViewModel = new ProjektViewModel
            {
                Id = projekt.Id,
                Titel = projekt.Titel,
                Beskrivning = projekt.Beskrivning,
                Skapare = projekt.User?.UserName ?? "Okänd Skapare", // Hantera nullvärden
                DeltagandeAnvändare = deltagandeAnvändareViewModels // Använd den konverterade listan här
            };

            return View(projektViewModel);
        }

        public IActionResult CreateProject()
        {
            return View();
        }

        public IActionResult ChangeProject()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CreateProject(CreateProjectViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Get the user ID of the currently logged-in user
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                // Create a new Project instance
                var newProject = new Projekt
                {
                    Titel = model.Titel,
                    Beskrivning = model.Beskrivning,
                    AnvändarId = userId // Assign the logged-in user as the project creator
                };

                // Add the new project to the database
                _dbContext.Projekts.Add(newProject);
                _dbContext.SaveChanges();

                // Redirect to the ProjectPage
                return RedirectToAction("ProjectPage");
            }

            // If the model is not valid,
            // return to the CreateProject view with errors
            return View(model);
        }

        // Action-metod för att visa ändringssidan för ett projekt
        public IActionResult Change(int id)
        {
            var project = _dbContext.Projekts.FirstOrDefault(p => p.Id == id);

            if (project == null)
            {
                return NotFound();
            }

            var viewModel = new ChangeProjectViewModel
            {
                Id = project.Id,
                Titel = project.Titel,
                Beskrivning = project.Beskrivning
                // Fyll i andra fält om det behövs
            };

            return View(viewModel);
        }

        // Action-metod för att spara ändringarna för ett projekt
        [HttpPost]
        public IActionResult SaveChanges(ChangeProjectViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var project = _dbContext.Projekts.FirstOrDefault(p => p.Id == viewModel.Id);

                if (project == null)
                {
                    return NotFound();
                }

                // Uppdatera projektet med nya värden från vyn
                project.Titel = viewModel.Titel;
                project.Beskrivning = viewModel.Beskrivning;
                // Uppdatera andra fält vid behov

                _dbContext.SaveChanges(); // Spara ändringarna till databasen

                return RedirectToAction("ProjectPage"); // Eller en annan åtgärd efter att ändringarna har sparats
            }

            // Om ModelState inte är giltig, returnera vyn med felmeddelanden
            return View("ChangeProject", viewModel);
        }



    }


}
