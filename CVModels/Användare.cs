using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;


namespace CVModels
{
    //[Table("user")]
    public class Användare : IdentityUser
    {
        public string? Profilbild { get; set; }
        public bool Aktiv { get; set; }
        public virtual Person? Person { get; set; }
        public virtual CV? Cv { get; set; }
        public virtual Profile? Profil { get; set; }
        [NotMapped]

        public override string? Id { get; set; }
        public string? Namn { get; set; }
        public string? ProfilbildUrl { get; set; }
        public string? Kompetenser { get; set; }

        public string? Discriminator { get; set; }
        public virtual IEnumerable<Meddelande>? MottagnaMeddelanden { get; set; }
        public virtual IEnumerable<DeltarProjekt>? DeltarIProjekt { get; set; }
        public virtual IEnumerable<Projekt>? SkapadeProjekt { get; set; }
        
        [InverseProperty("User")]
        public virtual ICollection<CV>? Cvs { get; set; }

    }
}
