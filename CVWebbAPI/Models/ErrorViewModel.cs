//Denna kod definierar en enkel modell, ErrorViewModel,
//som inneh�ller en egenskap RequestId f�r att lagra en beg�rande identifierare och
//en metod ShowRequestId f�r att kontrollera om RequestId �r giltig och ska visas.

namespace CVWebbAPI.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
