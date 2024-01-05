using CVModels.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


// StartPageViewModel: Används för att strukturera data som ska visas på
// startsidan, inkluderar vanligtvis listor av CVs och projekt.

public class StartPageViewModel
{
    public IEnumerable<CVViewModel> CVs { get; set; }
    public ProjektViewModel LatestProject { get; set; }

    public IEnumerable<AnvändareViewModel> Användare { get; set; }
}