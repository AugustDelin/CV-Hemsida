using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
	public class AnvändareCVViewModel
	{

		public List<AnvändareViewModel> Användare { get; set; }
		// Andra relevanta egenskaper...

		public int Id { get; set; }
		public string Namn { get; set; }
		// Andra CV-relaterade egenskaper...

	}
}
