//Klassen Meddelande definierar egenskaper för meddelanden såsom
//innehåll, avsändare, mottagare och läst-status.
//Den använder en främmande nyckel för att skapa en relation till en
//användare (Användare) genom dess mottagar-ID i en databastabell.

using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    public class Meddelande
    {
        public int Id { get; set; }
        public virtual string Innehåll { get; set; }
        public virtual string Avsändare { get; set; }
        public virtual string Mottagare { get; set; }
        public bool Läst { get; set; }

        [ForeignKey(nameof(Mottagare))]
        public virtual Användare Användare { get; set; }
    }
}
