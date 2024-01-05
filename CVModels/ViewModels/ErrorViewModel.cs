using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace CVModels.ViewModels
{

    // ErrorViewModel: Används för att visa felmeddelanden och teknisk
    // information vid applikationsfel.

    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}