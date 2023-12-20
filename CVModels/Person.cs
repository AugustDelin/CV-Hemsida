using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels
{
	public class Person
	{
        [Key]
        [Required]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public string Personnummer { get; set; }
        [Required]
        public string Förnamn { get; set; }
        [Required]
        public string Efternamn { get; set; }
        [Required]
        public string Adress { get; set; }

        public virtual string AnvändarID { get; set; }

        [ForeignKey(nameof(AnvändarID))]
        public virtual Användare User { get; set; }

        public string FullName()
        {
            return Förnamn + ' ' + Efternamn;
        }

    }
}
