using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels
{
    public class CVViewModel
    {
        public int Id { get; set; }
        public string AnvändarId { get; set; }
        public string AnvändarNamn { get; set; }
        public string Kompetenser { get; set; }
        public string Utbildningar { get; set; }
        public string TidigareErfarenhet { get; set; }
    }
}
