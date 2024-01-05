using System.ComponentModel.DataAnnotations;

namespace CVModels.ViewModels
{

    // ChangeProjectViewModel: Tillhandahåller nödvändig datastruktur för att
    // tillåta användare att ändra information om ett projekt.

    public class ChangeProjectViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Titel är obligatorisk")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatorisk")]
        public string Beskrivning { get; set; }

        // Om du har fler attribut eller behöver valideringar för redigering av projektet, kan de läggas till här
    }
}
