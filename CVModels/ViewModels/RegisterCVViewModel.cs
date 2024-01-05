using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    // RegisterCVViewModel: Tillhandahåller data och valideringsregler för att
    // registrera ett nytt CV.

    public class RegisterCVViewModel
    {
        [Required(ErrorMessage = "Fyll i kompetenser")]
       
        public string Kompetenser { get; set; }

        [Required(ErrorMessage = "Fyll i utbildningar")]
        
        public string? Utbildningar { get; set; }

        [Required(ErrorMessage = "Fyll i tidigare erfarenheter")]
        
        public string? TidigareErfarenhet { get; set; }

        public string? ProfilbildPath { get; set; }
    }
}
