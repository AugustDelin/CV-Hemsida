using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class AnvändareCVViewModel
    {
        public string Id { get; set; }
        public string Namn { get; set; }
        public string ProfilbildPath { get; set; }
        public string Kompetenser { get; set; }
        public string Utbildningar { get; set; }
        public string TidigareErfarenhet { get; set; }
        public List<ProjektViewModel> DeltarIProjekt { get; set; }

        // Lägg till andra egenskaper som behövs för CV-sidan
    }
}