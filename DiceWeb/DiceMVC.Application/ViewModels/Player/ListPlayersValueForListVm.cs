using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class ListPlayersValueForListVm
    {
        public List<PlayerValueForListVm> AllPlayersValue { get; set; }
        int Count { get; set; }
    }
}
