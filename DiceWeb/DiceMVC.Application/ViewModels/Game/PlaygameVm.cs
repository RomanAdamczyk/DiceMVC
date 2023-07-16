using DiceMVC.Application.ViewModels.Player;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiceMVC.Application.ViewModels.Game
{
    public class PlaygameVm
    {
        public int GameId { get; set; }
        public PlayerValueVM CurrentPlayer { get; set; }
        public List<PlayerScoreVm> Players { get; set; }
        public int PlayersCount { get; set; }
        public int Round { get; set; }
        public int Lap { get; set; }
        public List<DicesVm> Dices { get; set; }
        public DicesVm BlockedDices { get; set; }
        public PlayerValueVM OptionalValues { get; set; }

    }
}
