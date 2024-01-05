using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{

    // AnvändareViewModel: Används för att representera och hantera
    // data relaterad till användarprofiler i applikationen.


    public class AnvändareViewModel
    {

        public string Id { get; set; }

        public string Kompetenser { get; set; }

        public string? ProfilbildPath { get; set; }

        public string Namn { get; set; }
        public string? Profilbild { get; set; }
        public virtual Person? Person { get; set; }
        public virtual CV? Cv { get; set; }
        public virtual Profile? Profil { get; set; }
        [NotMapped]
        public virtual IEnumerable<Meddelande>? MottagnaMeddelanden { get; set; }
        public virtual IEnumerable<DeltarProjekt>? DeltarIProjekt { get; set; }
        public virtual IEnumerable<Projekt>? SkapadeProjekt { get; set; }

        [InverseProperty("User")]
        public virtual ICollection<CV>? Cvs { get; set; }

    }
}
