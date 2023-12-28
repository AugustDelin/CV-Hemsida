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
