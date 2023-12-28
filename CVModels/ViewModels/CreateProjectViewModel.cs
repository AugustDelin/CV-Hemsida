using System.ComponentModel.DataAnnotations;

namespace CVModels.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = "Titel är obligatorisk")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatorisk")]
        public string Beskrivning { get; set; }


        // Om du har fler attribut eller behöver valideringar för projektet, kan de läggas till här
    }
}
