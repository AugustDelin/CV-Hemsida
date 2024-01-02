using System.ComponentModel.DataAnnotations;

public class ChangeInformationViewModel
{
    [Required(ErrorMessage = "Förnamn är obligatoriskt")]
    [StringLength(50, ErrorMessage = "Förnamnet får inte vara längre än 50 tecken")]
    public string Förnamn { get; set; }

    [Required(ErrorMessage = "Efternamn är obligatoriskt")]
    [StringLength(50, ErrorMessage = "Efternamnet får inte vara längre än 50 tecken")]
    public string Efternamn { get; set; }

    [Required(ErrorMessage = "Adress är obligatorisk")]
    [StringLength(100, ErrorMessage = "Adressen får inte vara längre än 100 tecken")]
    public string Adress { get; set; }

    public bool ÄrProfilPrivat { get; set; }
}
