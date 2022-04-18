using DiceMVC.Application.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class GetSavedGamesToListVm
    {
        public int GameId { get; set; }
        public List<PlayerValueForListVm> Players { get; set; }
        public int Count { get; set; }
    }
}
