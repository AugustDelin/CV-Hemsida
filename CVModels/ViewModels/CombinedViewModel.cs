using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{

    // CombinedViewModel: Kombinerar flera `ViewModels` för att
    // presentera sammanställd information på en och samma vy.

    public class CombinedViewModel
    {
        public IEnumerable<CVViewModel> CVs { get; set; }
        public StartPageViewModel StartPage { get; set; }
    }
}