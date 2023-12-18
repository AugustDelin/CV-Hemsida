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
        public int Id { get; set; }

        public string Kompetenser { get; set; }

        public string Utbildningar { get; set; }

        public string TidigareErfarenhet { get; set; }

        public string ProfilbildPath { get; set; }

        public virtual ICollection<Projekt> Projekts { get; set; }
       
        [Required]
        public string UserId { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual User User { get; set; }
    }
}
