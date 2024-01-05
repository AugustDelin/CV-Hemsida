using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class RegisterUserViewModel
    {
        [Required(ErrorMessage = "Skriv in ett användarnamn")]
        [StringLength(100)]
        public string Email { get; set; }

        [Required(ErrorMessage = "Skriv in ett lösenord mellan 3 och 20 tecken")]
        [StringLength(20, MinimumLength = 3)]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required(ErrorMessage = "Du behöver bekräfta ditt lösenord")]
        [DataType(DataType.Password)]
        [Display(Name = "Bekräfta ditt lösen")]
        [Compare("Password", ErrorMessage = "Lösenorden stämmer inte överrens!")]
        public string ConfirmPassword { get; set; }

    }
}

