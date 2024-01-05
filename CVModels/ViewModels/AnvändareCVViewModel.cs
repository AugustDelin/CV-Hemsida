using System.Collections.Generic;

namespace CVModels.ViewModels
{

    // AnvändareCVViewModel används för att representera data på en
    // användares CV-sida, inklusive personlig information och projekt.

    public class AnvändareCVViewModel
    {

        public string? ProfilbildPath { get; set; }

        public string Id { get; set; }
        public string Namn { get; set; }
        public string Kompetenser { get; set; }
        public string Utbildningar { get; set; }
        public string TidigareErfarenhet { get; set; }
        public List<ProjektViewModel> DeltarIProjekt { get; set; }
        // Lägg till fler egenskaper efter behov
    }
}

