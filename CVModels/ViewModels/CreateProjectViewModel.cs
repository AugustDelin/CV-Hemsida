using System.ComponentModel.DataAnnotations;

namespace CVModels.ViewModels
{
    public class CreateProjectViewModel
    {
        [Required(ErrorMessage = "Titel är obligatorisk")]
        public string Titel { get; set; }

        [Required(ErrorMessage = "Beskrivning är obligatorisk")]
        public string Beskrivning { get; set; }
    }
}
