//Den här koden definierar en klass, DeltarProjekt,
//som representerar en kopplingstabell i databasen mellan användare och projekt.
//Den använder Entity Framework Core-attribut för att definiera primärnyckeln och
//utländska nycklar som pekar på Användare och Projekt för att skapa relationer mellan tabellerna.

using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CVModels
{
    [PrimaryKey(nameof(Deltagare), nameof(Projekt))]
    public class DeltarProjekt
    {
        public virtual string Deltagare { get; set; }
        public virtual int Projekt { get; set; }

        
        [ForeignKey(nameof(Deltagare))]
        public virtual Användare Anv { get; set; }

        [ForeignKey(nameof(Projekt))]
        public virtual Projekt Proj { get; set; }
    }
}
