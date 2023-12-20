using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class RegisterPersonViewModel
    {
        [Required(ErrorMessage = "Skriv in personnummer")]
        public string Personnummer { get; set; }

        [Required(ErrorMessage = "Skriv in förnamn")]
        public string Förnamn { get; set; }

        [Required(ErrorMessage = "Skriv in efternamn")]
        public string Efternamn { get; set; }

        [Required(ErrorMessage = "Skriv in adress")]
        public string Adress { get; set; }
    }
}
