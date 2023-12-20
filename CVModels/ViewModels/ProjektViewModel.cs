using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class ProjektViewModel
    {
        public int Id { get; set; }
        public string Titel { get; set; }
        public string Beskrivning { get; set; }
        // Andra egenskaper som behövs, exempelvis en lista med deltagande användare
    }
}