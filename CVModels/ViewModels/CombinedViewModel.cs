using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class CombinedViewModel
    {
        public IEnumerable<CVViewModel> CVs { get; set; }
        public StartPageViewModel StartPage { get; set; }
    }
}
