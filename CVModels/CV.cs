//Den här koden definierar en CV-klass som representerar en användares CV-data.
//Den innehåller egenskaper för kompetenser, utbildningar, tidigare erfarenheter och profilbildens sökväg.
//Dessutom håller den en samling av projekt som användaren deltar i och
//relaterar varje CV till en specifik användare genom en nyckel.

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels
{
    public class CV
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Kompetenser { get; set; }

        public string? Utbildningar { get; set; }

        public string? TidigareErfarenhet { get; set; }

        public string? ProfilbildPath { get; set; }

        public virtual ICollection<DeltarProjekt> DeltarIProjekt { get; set; }


        [Required]
        public string AnvändarId { get; set; }

        [ForeignKey(nameof(AnvändarId))]
        [InverseProperty("Cvs")]
        public virtual Användare User { get; set; } 
    }
}
