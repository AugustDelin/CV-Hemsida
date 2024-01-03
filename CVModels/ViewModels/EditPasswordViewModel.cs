using System.ComponentModel.DataAnnotations;

namespace CVModels.ViewModels
{
    public class EditPasswordViewModel
    {
     

        [Required(ErrorMessage = "Nuvarande lösenord krävs")]
        [DataType(DataType.Password)]
        [Display(Name = "Nuvarande lösenord")]
        public string NuvarandeLösenord { get; set; }

        [Required(ErrorMessage = "Nytt lösenord krävs")]
        [StringLength(100, ErrorMessage = "{0} måste vara minst {2} och max {1} tecken långt.", MinimumLength = 3)]
        [DataType(DataType.Password)]
        [Display(Name = "Nytt lösenord")]
        public string NyttLösenord { get; set; }

        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta nytt lösenord")]
        [Compare("NyttLösenord", ErrorMessage = "Det nya lösenordet och bekräftelsen stämmer inte överens.")]
        public string BekräftaNyttLösenord { get; set; }
    }
}
