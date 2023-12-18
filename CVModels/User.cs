using Microsoft.AspNetCore.Identity;
using System.Collections;
using System.ComponentModel.DataAnnotations.Schema;


namespace CVModels
{
	public class User : IdentityUser
    {
		public string Password { get; set; }
		public string UserName { get; set; }
		public bool Active { get; set; }
		







	}
}
