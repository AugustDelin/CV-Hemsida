//Denna kod definierar en enkel modell, ErrorViewModel,
//som innehåller en egenskap RequestId för att lagra en begärande identifierare och
//en metod ShowRequestId för att kontrollera om RequestId är giltig och ska visas.

namespace CVWebbAPI.Models
{
	public class ErrorViewModel
	{
		public string? RequestId { get; set; }

		public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
	}
}
