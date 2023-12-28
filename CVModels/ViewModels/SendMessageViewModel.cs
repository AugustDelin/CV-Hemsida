using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class SendMessageViewModel
    {
        [Required(ErrorMessage = "Du måste ha innehåll.")]
        public virtual string Innehåll { get; set; }

        [Required(ErrorMessage = "Du måste ange ett namn.")]
        public virtual string Avsändare { get; set; }
        public virtual string Mottagare { get; set; }



    }
}

