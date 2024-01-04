using CVModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class StartPageViewModel
{
    public IEnumerable<CVViewModel> CVs { get; set; }
    public ProjektViewModel LatestProject { get; set; }

    public IEnumerable<AnvändareViewModel> Användare { get; set; }
}