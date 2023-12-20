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
