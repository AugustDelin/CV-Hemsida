using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModel
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Skriv in ett användarnamn")]
        [StringLength(100)]

        public string UserName { get; set; }


        [Required(ErrorMessage = "Skriv in lösenord")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember Me")]
        public bool Active { get; set; }

        public string? ErrorMessage { get; set; }
    }
}


