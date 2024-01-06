//Klassen Profile innehåller egenskaper för en profil, inklusive
//ett identifieringsnummer, en beskrivning och en indikator för privatinställningar.
//Den är kopplad till en användare genom en främmande nyckel och
//spårar antalet klick på profilen.

using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    public class Profile
    {
        public int Id { get; set; }
        public int AntalKlick { get; set; }
        public string? Beskrivning { get; set; }
        public bool Privat { get; set; }

        public virtual string? Användare { get; set; }

        [ForeignKey(nameof(Användare))]
        public virtual Användare Anv { get; set; }
    }
}
