using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;


namespace CVModels
{
	[Table ("users")]
	public class User : IdentityUser
    {
        public string? Profilbild { get; set; }
        public bool Aktiv { get; set; }

    }
}
