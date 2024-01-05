using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{

    // ChangeCVViewModel: Används för att hantera data vid uppdatering
    // av en användares CV-information.

    public class ChangeCVViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Fyll i kompetenser")]
        
        public string Kompetenser { get; set; }

        [Required(ErrorMessage = "Fyll i utbildningar")]
        
        public string? Utbildningar { get; set; }

        [Required(ErrorMessage = "Fyll i tidigare erfarenheter")]
        
        public string? TidigareErfarenhet { get; set; }

        public string? ProfilbildPath { get; set; }


    }
}
