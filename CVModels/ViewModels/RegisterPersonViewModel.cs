using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{

    // RegisterPersonViewModel: Används för att samla in och validera
    // personlig information vid registrering av en ny person i systemet.

    public class RegisterPersonViewModel
    {
        [Required(ErrorMessage = "Skriv in personnummer")]
        [RegularExpression("^[0-9]+$", ErrorMessage = "Endast nummer är tillåtet på personnummret")]
        public string Personnummer { get; set; }

        [Required(ErrorMessage = "Skriv in förnamn")]
        public string Förnamn { get; set; }

        [Required(ErrorMessage = "Skriv in efternamn")]
        public string Efternamn { get; set; }

        [Required(ErrorMessage = "Skriv in adress")]
        public string Adress { get; set; }
    }
}
