using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVModels.ViewModels;

public class ProfileViewModel
{
    public string FullName { get; set; }
    public string UserName { get; set; }
    public string Address { get; set; }
    public bool IsSelf { get; set; }
    public string CurrentUserFullName { get; set; }
    public string Message { get; set; }
}
