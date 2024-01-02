using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class ChangeCVViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Fyll i kompetenser")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Endast bokstäver och siffror är tillåtna.")]
        public string Kompetenser { get; set; }

        [Required(ErrorMessage = "Fyll i utbildningar")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Endast bokstäver och siffror är tillåtna.")]
        public string? Utbildningar { get; set; }

        [Required(ErrorMessage = "Fyll i tidigare erfarenheter")]
        [RegularExpression(@"^[a-zA-Z0-9\s]+$", ErrorMessage = "Endast bokstäver och siffror är tillåtna.")]
        public string? TidigareErfarenhet { get; set; }

        public string? ProfilbildPath { get; set; }


    }
}
