//Klassen Projekt representerar projekt och
//inkluderar attribut som en unik identifierare, titel, beskrivning och
//en referens till den användare som skapat projektet.
//Dessutom innehåller den en samling av användare som deltar i projektet.

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
        [Column("Id")]
        public int Id { get; set; }

        public string Titel { get; set; }

        public string Beskrivning { get; set; }

        [Required]
        public string AnvändarId { get; set; }

        [ForeignKey(nameof(AnvändarId))]
        public virtual Användare User { get; set; }


        // Antagande: en samling av användare som deltar i projektet
        public virtual ICollection<Användare> DeltagandeAnvändare { get; set; }


    }
}
