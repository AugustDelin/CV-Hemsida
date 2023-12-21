using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels
{
    public class Projekt
    {
        [Key]
        public int Id { get; set; }

        public string Titel { get; set; }

        public string Beskrivning { get; set; }

        [Required]
        public string AnvändarId { get; set; }

        [ForeignKey(nameof(AnvändarId))]
        public virtual Användare User { get; set; }

     
    }
}
