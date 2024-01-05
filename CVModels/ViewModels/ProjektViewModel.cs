using CVModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{

    // ProjektViewModel: Innehåller detaljerad information om ett specifikt
    // projekt och dess deltagare.

    public class ProjektViewModel
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        // Andra egenskaper som behövs, exempelvis en lista med deltagande användare
        public List<AnvändareViewModel> DeltagandeAnvändare { get; set; }

        public string Skapare{ get; set; }

    }
}
