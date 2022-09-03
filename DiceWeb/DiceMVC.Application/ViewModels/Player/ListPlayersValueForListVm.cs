using DiceMVC.Application.ViewModels.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Player
{
    public class ListPlayersValueForListVm
    {
        public List<PlayerValueVM> AllPlayersValue { get; set; }
        public int Count { get; set; }
    }
}
