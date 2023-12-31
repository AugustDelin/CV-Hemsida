﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModel
{

    // LoginViewModel: Innehåller inloggningsdata och valideringsregler för
    // användarautentisering.
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Skriv in ett användarnamn")]
        [StringLength(100)]

        public string UserName { get; set; }


        [Required(ErrorMessage = "Skriv in lösenord")]
        [DataType(DataType.Password)]
        public string Lösenord { get; set; }

        [Display(Name = "Remember Me")]
        public bool Active { get; set; }

        public string? ErrorMessage { get; set; }

       
    }
}


