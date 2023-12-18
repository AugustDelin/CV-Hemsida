using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels
{
    public class Projekt
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        public string AnvändarID { get; set; }

        [ForeignKey(nameof(AnvändarID))]
        public virtual User Användare { get; set; }


    }
}
